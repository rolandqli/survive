using UnityEngine;

public class Exp : MonoBehaviour
{
    // Access to outside
    private Transform playerTransform;

    // Physics
    private Rigidbody2D rb;

    // Amount of XP
    private float amount = 0f;

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
    }

    public void setAmount(float number)
    {
        Debug.Log(amount);
        amount = number;
        Debug.Log(amount);
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
            rb.bodyType = RigidbodyType2D.Dynamic;

            Transform initial_position = transform;
            rb.linearVelocity = -(initial_position.position - playerTransform.position).normalized * 100;
        }
        else if (collidedObject.name == "Player")
        {
            // Upon colliding with player, they should receive xp
            Debug.Log("Leveling Up! Sending " + amount);
            Player player = collidedObject.GetComponent<Player>();
            player.increaseEXP(amount);

            // Disappear
            Destroy(this.gameObject);
        }

    }

    void OnCollisionEnter2D(Collision2D other)
    {
        
    }
}

