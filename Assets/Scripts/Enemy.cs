using UnityEngine;

public class Enemy : MonoBehaviour
{
    private int currHP;
    Rigidbody2D EnemyRB;
    private Transform player;
    public int maxHP = 1;
    private float moveMult;
    SpawnManager spawnManager;
    public GameObject expOrb;


    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void OnCollisionEnter2D(Collision2D other)
    {
        GameObject collidedObject = other.gameObject;
        if (collidedObject.name == "Player")
        {
            Debug.Log("i got hit");
            Player hitPlayer = collidedObject.GetComponent<Player>();
            hitPlayer.takeDamage(maxHP);
        }

    }

    private void Awake() 
    { 
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
        Transform initial_position = EnemyRB.transform;
        EnemyRB.linearVelocity = -(initial_position.position - player.position).normalized * 10 * moveMult;
        //Debug.Log(EnemyRB.velocity);

    }

    private void Die()
    {
        spawnManager.enemyDeath();
        expOrb.transform.position = transform.position;
        expOrb.GetComponent<Exp>().setAmount(10f);
        Instantiate(expOrb);

        Destroy(this.gameObject);
        
    } 
    //void OnMouseDown()
    //{
    //    Debug.Log("Enemy clicked: " + gameObject.name);

    //    Die();
    //}

}
