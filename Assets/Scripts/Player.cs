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
    bool isAttacking;
    Vector2 currDirection;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        currHP = maxHP;
        HPSlider.value = 1;
        EXPSlider.value = 0;
    }

    public void increaseDamage(int addedDamage)
    {
        damage += addedDamage;
    }

    public void increaseEXP(float amount)
    {
        exp += amount;
        Debug.Log(exp);
        if (exp >= expForNextLevel)
        {
            exp = exp % expForNextLevel;
            level += 1;
            expForNextLevel += (level - 1) * 50;
            levelText.text = level.ToString();
        }
        EXPSlider.value = exp / expForNextLevel;

    }

    public void takeDamage(int damage)
    {
        // Take damage
        currHP -= damage;
        Debug.Log(currHP);
        Debug.Log(maxHP);
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
        else if (attackTimer >= 0)
        {
            attackTimer -= Time.deltaTime;
        }
        anim.SetFloat("DirX", currDirection.x);
        anim.SetFloat("DirY", currDirection.y);
    }

   
    private void Attack()
    {
        StartCoroutine(AttackRoutine());
        attackTimer = attackSpeed;
    }



    IEnumerator AttackRoutine()
    {
        isAttacking = true;
        anim.SetTrigger("Attack");
        //FindObjectOfType<AudioManager>().Play("PlayerAttack");


        yield return new WaitForSeconds(hitboxTiming);
        RaycastHit2D[] hits = Physics2D.BoxCastAll(rb.position + currDirection, Vector2.one * 20, 0f, Vector2.zero);

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.transform.CompareTag("Enemy"))
            {
                Debug.Log("hit");
                Enemy enemy = hit.collider.gameObject.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.takeDamage(damage);
                }
            }
        }

        yield return new WaitForSeconds(hitboxTiming);
        isAttacking = false;
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
