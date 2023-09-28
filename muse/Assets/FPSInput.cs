using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[AddComponentMenu("Control Script/FPS Input")]
public class FPSInput : MonoBehaviour
{
    public float speed = 6.0f;
    public float gravity = -9.8f;
    private CharacterController charController;
    private float verticalVelocity = 0.0f; // Initialize verticalVelocity

    void Start()
    {
        charController = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Get the camera's forward direction
        Vector3 cameraForward = Camera.main.transform.forward;
        cameraForward.y = 0; // Zero out the Y component to ensure movement stays horizontal.
        cameraForward.Normalize(); // Normalize to maintain consistent speed.

        // Get the camera's right direction (perpendicular to forward)
        Vector3 cameraRight = Camera.main.transform.right;
        cameraRight.y = 0;
        cameraRight.Normalize();

        // Calculate movement direction based on input
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        Vector3 movement = (cameraForward * moveZ + cameraRight * moveX).normalized;

        // Apply gravity
        verticalVelocity = charController.isGrounded ? -0.5f : verticalVelocity + gravity * Time.deltaTime;
        movement.y = verticalVelocity;

        // Move the character using CharacterController
        charController.Move(movement * speed * Time.deltaTime);
    }
}
