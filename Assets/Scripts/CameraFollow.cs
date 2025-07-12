using UnityEngine;

// It's possible we may not need this class at all (there's built-in thing apparently)
public class CameraFollow : MonoBehaviour
{
    public Transform target;       // The player
    public Vector3 offset = new Vector3(0, 0, -10);  // Offset from the player
    public float smoothSpeed = 5f; // Camera move speed

    void FixedUpdate()
    {
        if (target == null) return;

        // the offset is necessary because it needs to be away from the player on the z axis
        Vector3 desiredPosition = target.position + offset;
        if (transform.position != desiredPosition)
        {
            // transforms and follows player
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
            transform.position = smoothedPosition;
        }
        
    }
}
