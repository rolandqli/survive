using UnityEngine;
using System.Collections;


public class BlueTiger : Enemy
{
    public float shootTimer = 1;
    public GameObject enemyProjectile;
    float hitboxTiming = 0.1f;


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
        GameObject newProjectile = Instantiate(enemyProjectile, transform.position, transform.rotation);
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

    protected override void Die()
    {
        // Notifies manager of death
        spawnManager.enemyDeath();

        // Spawns orb
        GameObject newOrb = Instantiate(expOrb);
        //Debug.Log("setting exp");
        newOrb.GetComponent<Exp>().setAmount(30f);
        newOrb.transform.position = transform.position;


        // Disappears
        Destroy(this.gameObject);

    }
}
