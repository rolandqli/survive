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
        rb.linearVelocity = transform.right * speed; 
        Destroy(gameObject, lifeTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Hit: " + other.name);
        if (other.gameObject.transform.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<Enemy>().takeDamage(damage);
            Destroy(gameObject);
        }
    }
}
