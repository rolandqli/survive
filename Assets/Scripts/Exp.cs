using UnityEngine;

public class Exp : MonoBehaviour
{
    // Access to outside
    private Transform playerTransform;

    // Physics
    private Rigidbody2D rb;

    // Amount of XP
    private float amount = 10;

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
    }

    public void setAmount(float number)
    {
        amount = number;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Magnet is a trigger so we use this
    void OnTriggerEnter2D(Collider2D other)
    {
        GameObject collidedObject = other.gameObject;
        if (collidedObject.name == "Magnet")
        {
            // Upon colliding with magnet collider, the orb should move to player
            Transform initial_position = transform;
            rb.linearVelocity = -(initial_position.position - playerTransform.position).normalized * 100;
        }

    }

    void OnCollisionEnter2D(Collision2D other)
    {
        GameObject collidedObject = other.gameObject;
        if (collidedObject.name == "Player")
        {
            // Upon colliding with player, they should receive xp
            Player player = collidedObject.GetComponent<Player>();
            player.increaseEXP(amount);

            // Disappear
            Destroy(this.gameObject);
        }
    }
}

