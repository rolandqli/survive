using UnityEngine;

public class Exp : MonoBehaviour
{
    private Transform playerTransform;
    private Rigidbody2D rb;
    private float amount = 10;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
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

    void OnTriggerEnter2D(Collider2D other)
    {
        GameObject collidedObject = other.gameObject;
        if (collidedObject.name == "Magnet")
        {
            Debug.Log("Magnetized!");
            Transform initial_position = transform;
            rb.linearVelocity = -(initial_position.position - playerTransform.position).normalized * 100;
        }

    }

    void OnCollisionEnter2D(Collision2D other)
    {
        GameObject collidedObject = other.gameObject;
        if (collidedObject.name == "Player")
        {
            Debug.Log("Hit!");
            Player player = collidedObject.GetComponent<Player>();
            Debug.Log("increasingexp");
            player.increaseEXP(amount);
            Destroy(this.gameObject);
        }
    }
}

