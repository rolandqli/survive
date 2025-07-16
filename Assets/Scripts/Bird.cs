using UnityEngine;
using System.Collections;

public class Bird : Enemy
{
    // Enemy jumps a certain distance 
    public float interpolationFramesCount = 45;
    public float duration = 1f;
    public float jumpTimer = 1;


    protected override void Update()
    {
        jumpTimer -= Time.deltaTime;
        if (jumpTimer < 0) {
            StartCoroutine(JumpDistance());
            jumpTimer = 1;
        }
        EnemyRB.linearVelocity = Vector2.zero;
    }

    IEnumerator JumpDistance()
    {
        Vector3 start = transform.position;
        Vector3 direction = (player.position - start).normalized;
        float elapsed = 0f;

        Vector3 target = direction * 10 + start;

        while (elapsed < duration)
        {
            transform.position = Vector3.Lerp(transform.position, target, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;

        }
        transform.position = target;

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
        newOrb.GetComponent<Exp>().setAmount(20f);
        newOrb.transform.position = transform.position;


        // Disappears
        Destroy(this.gameObject);

    }

}
