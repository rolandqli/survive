using UnityEngine;
using System.Collections;
//using System.Collections.Generic;
//using UnityEngine.UI;

public class Player : MonoBehaviour
{
    // Access Physics
    private Rigidbody2D rb;
    Vector2 movement;
    Animator anim;


    // Stats
    private int currHP;
    public int maxHP = 30;
    public int exp = 0;
    public int level = 1;
    public int luck = 30;
    public float moveSpeed = 5f;

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
    }

    public void takeDamage(int damage)
    {
        // Take damage
        currHP -= damage;

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

    void VisualizeBox(Vector2 center, Vector2 size, float angle)
    {
        // Calculate the four corners of the box
  

        Quaternion rot = Quaternion.Euler(0, 0, angle);
        Vector2 halfSize = size * 0.5f;

        // Convert center to Vector3
        Vector3 center3D = new Vector3(center.x, center.y, 0f);

        // Rotate corner offsets using Quaternion and convert to Vector3
        Vector3 topLeft = center3D + rot * new Vector3(-halfSize.x, halfSize.y, 0f);
        Vector3 topRight = center3D + rot * new Vector3(halfSize.x, halfSize.y, 0f);
        Vector3 bottomRight = center3D + rot * new Vector3(halfSize.x, -halfSize.y, 0f);
        Vector3 bottomLeft = center3D + rot * new Vector3(-halfSize.x, -halfSize.y, 0f);

        // Draw the box in Scene view
        Debug.DrawLine(topLeft, topRight, Color.red);
        Debug.DrawLine(topRight, bottomRight, Color.red);
        Debug.DrawLine(bottomRight, bottomLeft, Color.red);
        Debug.DrawLine(bottomLeft, topLeft, Color.red);
    }



    IEnumerator AttackRoutine()
    {
        isAttacking = true;
        anim.SetTrigger("Attack");
        //FindObjectOfType<AudioManager>().Play("PlayerAttack");
        Vector2 boxCenter = rb.position + currDirection;
        Vector2 boxSize = Vector2.one;
        float angle = 0f;

        VisualizeBox(boxCenter, boxSize, angle);


        yield return new WaitForSeconds(hitboxTiming);
        RaycastHit2D[] hits = Physics2D.BoxCastAll(rb.position + currDirection, Vector2.one /4, 0f, Vector2.zero);

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.transform.CompareTag("Enemy"))
            {
                /* TODO 3.2: Call TakeDamage() inside of the enemy's Enemy script using
                the "hit" reference variable */
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
