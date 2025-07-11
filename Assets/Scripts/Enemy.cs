using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int currHP;
    Rigidbody2D EnemyRB;
    public Transform player;
    public int maxHP = 1;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        /* TODO 2.2: Call Explode() if enemy comes in contact with player */
        GameObject collidedObject = other.gameObject;
        if (collidedObject.name == "Player")
        {
            Player hitPlayer = collidedObject.GetComponent<Player>();
            hitPlayer.takeDamage(maxHP);
        }

    }

    private void Awake()
    {
        EnemyRB = GetComponent<Rigidbody2D>();
        currHP = maxHP;
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            Move();
        }

    }

    private void Move()
    {
        /* TODO 2.1: Move the enemy towards the player */
        Transform initial_position = EnemyRB.transform;
        EnemyRB.linearVelocity = -(initial_position.position - player.position);
        //Debug.Log(EnemyRB.velocity);

    }



    private void Die()
    {
        Destroy(this.gameObject);
    }
}
