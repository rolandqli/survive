using UnityEngine;

public class Enemy : MonoBehaviour
{
    private int currHP;
    Rigidbody2D EnemyRB;
    private Transform player;
    public int maxHP = 1;
    private float moveMult;
    SpawnManager spawnManager;


    // Start is called once before the first execution of Update after the MonoBehaviour is created

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
        moveMult = 0.1f;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        spawnManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<SpawnManager>();
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
        EnemyRB.linearVelocity = -(initial_position.position - player.position) * moveMult;
        //Debug.Log(EnemyRB.velocity);

    }

    private void Die()
    {
        spawnManager.enemyDeath();
        Destroy(this.gameObject);
        
    }
    void OnMouseDown()
    {
        Debug.Log("Enemy clicked: " + gameObject.name);

        Die();
    }

}
