using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 20f;
    public float lifeTime = 3f;
    public int damage = 1;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = transform.right * speed; // Right is local "forward" in 2D
        Destroy(gameObject, lifeTime);
    }

    void OnTrigger2DEnter(Collider other)
    {
        // Optional: check tag or health component
        Debug.Log("Hit: " + other.name);
        if (other.gameObject.transform.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<Enemy>().takeDamage(damage);
        }
        Destroy(gameObject); // destroy on collision
    }
}
