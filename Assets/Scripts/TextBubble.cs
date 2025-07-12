
using TMPro;
using UnityEngine;

public class TextBubble : MonoBehaviour
{
    public Transform target; // The GameObject to follow
    public TextMeshProUGUI text; // The TMP text component

    public Vector3 offset = new Vector3(0, 2f, 0); // Offset above the object

    void LateUpdate()
    {
        if (target != null)
        {
            transform.position = target.position + offset;
            transform.LookAt(Camera.main.transform); // Face the camera
            transform.Rotate(0, 180, 0); // Optional: fix mirrored text
        }
    }

    public void SetText(string message)
    {
        text.text = message;
    }
}
