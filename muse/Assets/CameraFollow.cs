using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CameraFollow : MonoBehaviour
{
    public Transform target; // The character's Transform (assign it in the Inspector)
    public Vector3 offset = new Vector3(0f, 2f, -5f); // Adjust this to set the camera's position relative to the character
    public float smoothSpeed = 10f; // Adjust this to control the smoothness of camera movement

    void LateUpdate()
    {
        if (target == null)
        {
            // Make sure the target is assigned in the Inspector
            Debug.LogWarning("Camera target not assigned.");
            return;
        }

        // Calculate the desired camera position based on the target's position and offset
        Vector3 desiredPosition = target.position + offset;

        // Use Mathf.Lerp to smoothly interpolate between the current camera position and the desired position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        // Update the camera's position
        transform.position = smoothedPosition;

        // Make the camera look at the target (character)
        transform.LookAt(target);
    }
}
