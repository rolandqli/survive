using UnityEngine;
using System.Collections;
//using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Networking;

public class Player : MonoBehaviour
{
    // Access Physics
    private Rigidbody2D rb;
    Vector2 movement;
    Animator anim;
    public Slider HPSlider;
    public Slider EXPSlider;
    public TMP_Text levelText;
    public TextBubble textBubble;

    // Stats
    private float currHP;
    public float maxHP = 30;
    private float exp = 0;
    public float expForNextLevel = 30;
    private int level = 1;
    public int luck = 30;
    private float moveSpeed = 50f;

    // Attack variables (need to change this to be based on weapon in hand)
    public int damage = 3;
    public float attackSpeed = 1;
    private float attackTimer;
    public float hitboxTiming = 0.1f;
    public float endAnimationTiming = 0.1f;
    Vector2 currDirection;
    void Start()
    {
        // Init access
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        currHP = maxHP;
        // Init sliders
        HPSlider.value = 1;
        EXPSlider.value = 0;
        GameObject bubble = Instantiate(textBubblePrefab);
        textBubble = bubble.GetComponent<TextBubble>();
        tb.target = transform;
        tb.SetText("Hello, traveler!");
        StartCoroutine(requestToLLM("you are video game character fighting and killing wolves. you see a bunch coming at you. what do you say?Respond in a single line with only the answer. No introductions, no labels, no explanations."));
    }

    public void increaseDamage(int addedDamage)
    {
        damage += addedDamage;
    }

    public void increaseEXP(float amount)
    {
        exp += amount;

        // Logic for when hitting a level
        if (exp >= expForNextLevel)
        {
            exp = exp % expForNextLevel;
            level += 1;
            expForNextLevel += (level - 1) * 50;
            levelText.text = level.ToString();
        }
        EXPSlider.value = exp / expForNextLevel;

    }

    public void takeDamage(int damage)
    {
        // Take damage
        currHP -= damage;
        HPSlider.value = currHP / maxHP;

        // Die lower than 0 HP
        if (currHP <= 0)
        {
            Die();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Move based on input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // Face mouse position
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        currDirection = (mousePos - transform.position).normalized;

        float angle = Mathf.Atan2(currDirection.y, currDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        // Set animations
        if (movement.x != 0 && movement.y != 0)
        {
            anim.SetBool("Walk", true);
        }
        else
        {
            anim.SetBool("Walk", false);
        }

        // Attack if left click
        if (Input.GetMouseButtonDown(0) && attackTimer < 0)
        {
            Attack();
        }
        // idk if necessary, it just adds space between attacks
        else if (attackTimer >= 0)
        {
            attackTimer -= Time.deltaTime;
        }

        // Animation directions
        anim.SetFloat("DirX", currDirection.x);
        anim.SetFloat("DirY", currDirection.y);
    }

   
    private void Attack()
    {
        StartCoroutine(AttackRoutine());
        attackTimer = attackSpeed;
    }



    IEnumerator AttackRoutine()
    {
        // flag for nothing to happen
        anim.SetTrigger("Attack");

        // throws out a hit box
        yield return new WaitForSeconds(hitboxTiming);
        RaycastHit2D[] hits = Physics2D.BoxCastAll(rb.position + currDirection, Vector2.one * 20, 0f, Vector2.zero);

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.transform.CompareTag("Enemy"))
            {
                // if we hit enemy, we should do damage to it
                Enemy enemy = hit.collider.gameObject.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.takeDamage(damage);
                }
            }
        }

        yield return new WaitForSeconds(hitboxTiming);
    }

    void FixedUpdate()
    {
        // Move based on input
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    void Die()
    {
        Destroy(this.gameObject);
    }

    

    IEnumerator requestToLLM(string prompt)
    {
        Debug.Log(prompt);
        string url = "http://localhost:11434/api/generate";
        string json = $"{{\"model\":\"gemma3\",\"prompt\":\"{prompt}\",\"stream\":false}}"; ;
        Debug.Log(json);    
        byte[] body = System.Text.Encoding.UTF8.GetBytes(json);

        using (UnityWebRequest request = new UnityWebRequest(url, "POST"))
        {
            request.uploadHandler = new UploadHandlerRaw(body);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                string rawJson = request.downloadHandler.text;
                OllamaResponse response = JsonUtility.FromJson<OllamaResponse>(rawJson);

                string firstLine = response.response.Split('\n')[0].Trim();
                Debug.Log("LLM Says: " + firstLine);
            }
            else
            {
                Debug.LogError("Error: " + request.error);
            }
        }
    }
    [System.Serializable]
    public class OllamaResponse
    {
        public string model;
        public string response;
        public bool done;
    }
}
