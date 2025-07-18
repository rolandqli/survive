using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class Augment
{
    public string name;
    public Sprite icon;
    public string description;
    public UnityEvent onApply;

    public void Apply()
    {
        onApply?.Invoke(); // triggers any method(s) assigned in the Inspector
    }
}



public class AugmentButton : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public Image iconImage;
    public TextMeshProUGUI descriptionText;
    private Augment augment;
    private Button button;

    public void Setup(Augment augmentData, System.Action<Augment> onClickAction)
    {
        augment = augmentData;
        nameText.text = augment.name;
        iconImage.sprite = augment.icon;
        descriptionText.text = augment.description;

        button = GetComponent<Button>();
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => onClickAction?.Invoke(augment));
    }

    void Start()
    {
        //Augment targetAugment = allAugments[1];
        //Setup(targetAugment, applyAugment);
    }

    //void applyAugment(Augment targetAugment)
    //{
    //    targetAugment.Apply();
    //}
}
