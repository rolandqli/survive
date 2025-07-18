using UnityEngine;
using System.Collections;


public class BlueTiger : Enemy
{
    public float shootTimer = 3;
    public GameObject enemyProjectile;
    float hitboxTiming = 0.1f;
    SpriteRenderer sprite;

    protected override void Start()
    {
        base.Start();
        sprite = GetComponent<SpriteRenderer>();

    }

    protected override void Update()
    {
        shootTimer -= Time.deltaTime;
        if (shootTimer < 0)
        {
            StartCoroutine(ProjectileAttack());
            shootTimer = 1;
        }
        EnemyRB.linearVelocity = Vector2.zero;
    }

    IEnumerator ProjectileAttack()
    {
        // flag for nothing to happen
        //anim.SetTrigger("Attack");

        // throws out a hit box
        yield return new WaitForSeconds(hitboxTiming);
        Vector3 direction = player.position - transform.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        if (Mathf.Abs(angle) >= 90)
        {
            sprite.flipX = true;
        }
        else {
            sprite.flipX = false;
        }

        Quaternion projectileDirection = Quaternion.Euler(0, 0, angle);
        GameObject newProjectile = Instantiate(enemyProjectile, transform.position, projectileDirection);
        newProjectile.GetComponent<Projectile>().setSource("Enemy");

        yield return new WaitForSeconds(hitboxTiming);
    }
    //// Start is called once before the first execution of Update after the MonoBehaviour is created
    //void Awake()
    //{
    //    base.Awake();
    //}

    // Update is called once per frame
    //public GameObject expOrb;

}
