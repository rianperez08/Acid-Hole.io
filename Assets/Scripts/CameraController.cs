using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 0.125f;
    public float cameraDistance = 5.0f;
    public float cameraHeight = 2.0f;

    private Vector3 _cameraOffset;

    private void Start()
    {
        // Calculate the camera offset based on the initial camera position and the target position
        _cameraOffset = transform.position - target.position;
        _cameraOffset.y = 0.0f; // Set the y offset to zero to keep the camera at a fixed height
    }

    private void LateUpdate()
    {
        // Calculate the target position based on the player position and the camera offset
        Vector3 targetPosition = target.position + _cameraOffset;

        // Smoothly move the camera to the target position
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);
    }
}