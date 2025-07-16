using UnityEngine;
using UnityEngine.UI;


public class Augments : MonoBehaviour
{
    Player player;
    public GameObject[] augmentList;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Disappear();

        Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

    }

    void shuffle()
    {
        int index = Random.Range(0, augmentList.Length);
        GameObject firstAug = augmentList[index];
        firstAug.transform.position = editPosX(firstAug, -200);

        GameObject secondAug = augmentList[index];
        secondAug.transform.position = editPosX(secondAug, 0);

        GameObject thirdAug = augmentList[index];
        thirdAug.transform.position = editPosX(thirdAug, 200);

        //int index = Random.Range(0, augmentList.Length);

    }

    Vector3 editPosX(GameObject augButton, float xValue)
    {
        Vector3 initPosition = augButton.transform.position;
        initPosition.x = xValue;
        initPosition.y = 0;
        return initPosition;
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
