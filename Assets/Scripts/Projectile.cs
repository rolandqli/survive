using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 20f;
    public float lifeTime = 3f;
    public int damage = 1;
    private Rigidbody2D rb;
    private string source;
    Player player;
    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        rb.linearVelocity = transform.right * speed; 
        Destroy(gameObject, lifeTime);
    }

    public void setSource(string source)
    {
        this.source = source;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("Hit: " + other.name);
        if (source == "Enemy")
        {
            if (other.gameObject.transform.CompareTag("Player"))
            {
                other.gameObject.GetComponent<Player>().takeDamage(damage);
                Destroy(gameObject);
            }
        }
        else if (source == "Player")
        {
            if (other.gameObject.transform.CompareTag("Enemy"))
            {
                other.gameObject.GetComponent<Enemy>().takeDamage(damage);
                if (player.healOnHitStatus())
                {
                    player.Heal(1);
                }
                Destroy(gameObject);
            }
        }
        
    }
}
