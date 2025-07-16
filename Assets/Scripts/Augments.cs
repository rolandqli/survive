using UnityEngine;
using UnityEngine.UI;


public class Augments : MonoBehaviour
{
    Player player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Disappear();
        Button brutalAug = GameObject.Find("BrutalAug").GetComponent<Button>();

        Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

    }

    public void Disappear()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1f;
    }
    // Update is called once per frame
    void Update()
    {

    }

}
