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

        Vector3 target = direction * 20 + start;

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

}
