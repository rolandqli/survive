using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Stats
    private int currHP;
    public int maxHP = 1;
    // Adds randomness
    private float moveMult;

    // Physics
    protected Rigidbody2D EnemyRB;

    // Access to objects
    protected Transform player;
    protected SpawnManager spawnManager;
    public GameObject expOrb;

    private void OnCollisionEnter2D(Collision2D other)
    {
        GameObject collidedObject = other.gameObject;
        if (collidedObject.name == "Player")
        {
            //Debug.Log("i got hit");
            Player hitPlayer = collidedObject.GetComponent<Player>();
            hitPlayer.takeDamage(maxHP);
        }

    }

    public void Awake() 
    { 
        // Set variables
        EnemyRB = GetComponent<Rigidbody2D>();
        currHP = maxHP;
        moveMult = Random.Range(0f, 2f);
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

    private void Move()
    {
        // Gets direction vector then moves
        Transform initial_position = EnemyRB.transform;
        EnemyRB.linearVelocity = -(initial_position.position - player.position).normalized * 10 * moveMult;

    }

    private void Die()
    {
        // Notifies manager of death
        spawnManager.enemyDeath();

        // Spawns orb
        GameObject newOrb = Instantiate(expOrb);
        newOrb.GetComponent<Exp>().setAmount(10f);
        newOrb.transform.position = transform.position;


        // Disappears
        Destroy(this.gameObject);
        
    } 
    //void OnMouseDown()
    //{
    //    Debug.Log("Enemy clicked: " + gameObject.name);

    //    Die();
    //}

}
