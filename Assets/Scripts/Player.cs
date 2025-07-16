using UnityEngine;
using System.Collections;
//using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{
    // Access Physics
    private Rigidbody2D rb;
    Vector2 movement;
    Animator anim;
    public Slider HPSlider;
    public Slider EXPSlider;
    public TMP_Text levelText;
    public string attackStyle;
    public GameObject projectile;
    public Canvas augmentUI;
    private bool healOnHit = false;

    // Stats
    private float currHP;
    public float maxHP = 30;
    private float exp = 0;
    public float expForNextLevel = 30;
    private int level = 1;
    public int luck = 30;
    private float moveSpeed = 50f;

    // Attack variables (need to change this to be based on weapon in hand)
    public int damage = 3;
    public float attackSpeed = 1;
    private float attackTimer;
    public float hitboxTiming = 0.1f;
    public float endAnimationTiming = 0.1f;
    Vector2 currDirection;

    void Start()
    {
        // Init access
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        currHP = maxHP;
        // Init sliders
        HPSlider.value = 1;
        EXPSlider.value = 0;
    }

    public void increaseDamage(int addedDamage)
    {
        damage += addedDamage;
    }
    public void increaseSpeed(int addedSpeed)
    {
        moveSpeed += addedSpeed;
    }
    public void increaseHP(int addedHP)
    {
        currHP += addedHP;
        maxHP += addedHP;
    }

    public void increaseEXP(float amount)
    {

        //Debug.Log("Received" + amount.ToString()+"exp");
        exp += amount;

        // Logic for when hitting a level
        if (exp >= expForNextLevel)
        {
            exp = exp % expForNextLevel;
            level += 1;
            expForNextLevel += (level - 1) * 50;
            levelText.text = level.ToString();
            augmentUI.gameObject.GetComponent<Augments>().shuffle();
            augmentUI.gameObject.SetActive(true);
            Time.timeScale = 0f;
        }
        EXPSlider.value = exp / expForNextLevel;

    }

    public void takeDamage(int damage)
    {
        // Take damage
        currHP -= damage;
        HPSlider.value = currHP / maxHP;

        // Die lower than 0 HP
        if (currHP <= 0)
        {
            Die();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Move based on input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // Face mouse position
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        currDirection = (mousePos - transform.position).normalized;

        float angle = Mathf.Atan2(currDirection.y, currDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        // Set animations
        if (movement.x != 0 && movement.y != 0)
        {
            anim.SetBool("Walk", true);
        }
        else
        {
            anim.SetBool("Walk", false);
        }

        // Attack if left click
        if (Input.GetMouseButtonDown(0) && attackTimer < 0)
        {
            Attack();
        }
        // idk if necessary, it just adds space between attacks
        else if (attackTimer >= 0)
        {
            attackTimer -= Time.deltaTime;
        }

        // Animation directions
        anim.SetFloat("DirX", currDirection.x);
        anim.SetFloat("DirY", currDirection.y);
    }

   
    private void Attack()
    {
        if (attackStyle == "Melee") {
            StartCoroutine(AttackRoutine());
            attackTimer = attackSpeed;
        }
        else
        {
            StartCoroutine(ProjectileAttack());
            attackTimer = attackSpeed;
        }
    }

    IEnumerator ProjectileAttack()
    {
        // flag for nothing to happen
        anim.SetTrigger("Attack");

        // throws out a hit box
        yield return new WaitForSeconds(hitboxTiming);
        GameObject newProjectile = Instantiate(projectile, transform.position, transform.rotation);
        newProjectile.GetComponent<Projectile>().setSource("Player");

        yield return new WaitForSeconds(hitboxTiming);
    }


    IEnumerator AttackRoutine()
    {
        // flag for nothing to happen
        anim.SetTrigger("Attack");

        // throws out a hit box
        yield return new WaitForSeconds(hitboxTiming);
        RaycastHit2D[] hits = Physics2D.BoxCastAll(rb.position + currDirection, Vector2.one * 20, 0f, Vector2.zero);

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.transform.CompareTag("Enemy"))
            {
                // if we hit enemy, we should do damage to it
                Enemy enemy = hit.collider.gameObject.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.takeDamage(damage);
                    if (healOnHit) 
                    {
                        Heal(1);
                    }

                }
            }
        }

        yield return new WaitForSeconds(hitboxTiming);
    }

    public bool healOnHitStatus()
    {
        return healOnHit;
    }

    public void changeHealOnHit()
    {
        healOnHit = true;
    }

    public void Heal(float amount)
    {
        currHP += amount;
    }

    void FixedUpdate()
    {
        // Move based on input
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    void Die()
    {
        Destroy(this.gameObject);
    }
}
