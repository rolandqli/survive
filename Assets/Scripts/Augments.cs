using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;

public class Augments : MonoBehaviour
{
    int numAugs = 3;
    float[] positions = {-200f, 0f, 200f};
    public GameObject augButton;
    public Augment[] allAugments;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject newObj = newButton(augButton, positions[0]);
        newObj.GetComponent<AugmentButton>().Setup(allAugments[0], applyAugment);
    }
    void applyAugment(Augment targetAugment)
    {
        targetAugment.Apply();
    }

    public void shuffle()
    {

        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        Debug.Log("Destroyed Everything");
        Augment[] sample = allAugments.OrderBy(x => UnityEngine.Random.value).Take(numAugs).ToArray();
        Debug.Log("Length Sample: " + sample.Length.ToString());
        for (int i = 0; i < numAugs; i++)
        {
            Debug.Log("Loop Num: " + i.ToString());
            GameObject newObj = newButton(augButton, positions[i]);
            newObj.GetComponent<AugmentButton>().Setup(sample[i], applyAugment);

        }

    }

    GameObject newButton(GameObject augmentButton, float xVal)
    {
        GameObject newObj = Instantiate(augmentButton, transform);
        Debug.Log("Instantiated!");
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
