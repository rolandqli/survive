using UnityEngine;
using UnityEngine.UI;


public class Augments : MonoBehaviour
{
    Player player;
    public GameObject[] augmentList;
    GameObject firstButton;
    GameObject secondButton;
    GameObject thirdButton;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        firstButton = GameObject.Find("BrutalAug");
        secondButton = GameObject.Find("SpeedAug");

        thirdButton = GameObject.Find("HPAug");


        Disappear();

        Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

    }

    public void shuffle()
    {
        int index = Random.Range(0, augmentList.Length);
        GameObject firstAug = augmentList[index];
        Destroy(firstButton);
        firstButton = newButton(firstAug, -200);

        GameObject secondAug = augmentList[index];
        Destroy(secondButton);
        secondButton = newButton(secondAug, 0);
        //secondAug.transform.position = editPosX(secondAug, 0);

        GameObject thirdAug = augmentList[index];
        Destroy(thirdButton);
        thirdButton = newButton(thirdAug, 200);
        //thirdButton.transform.position = editPosX(thirdAug, 200);

        //int index = Random.Range(0, augmentList.Length);

    }

    GameObject newButton(GameObject augmentButton, int xVal)
    {
        GameObject newObj = Instantiate(augmentButton, transform);
        newObj.GetComponent<RectTransform>().anchoredPosition = new Vector2(xVal, 0);
        return newObj;
    }

    //void newButton(GameObject augment, GameObject original)
    //{
    //    IGameObject newObj = Instantiate(firstAug, original.transform.position, original.transform.rotation);
    //    Destroy(firstButton);
    //}

    Vector3 editPosX(GameObject augButton, float xValue)
    {
        Vector2 initPosition = new Vector2(xValue, 0);
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
