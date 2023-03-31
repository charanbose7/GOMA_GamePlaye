using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    public float parallaxSpeed; // The speed at which the background will move relative to the camera
    
    private Transform cameraTransform; // The transform of the camera in the scene
    private Vector3 lastCameraPosition; // The position of the camera in the previous frame
    
    private void Start()
    {
        cameraTransform = Camera.main.transform; // Get the transform of the main camera in the scene
        lastCameraPosition = cameraTransform.position; // Set the last camera position to the current position of the camera
    }

    private void Update()
    {
        float parallax = (lastCameraPosition.x - cameraTransform.position.x) * parallaxSpeed; // Calculate the parallax effect based on the camera's movement
        transform.position += new Vector3(parallax, 0f, 0f); // Move the background based on the parallax effect
        
        lastCameraPosition = cameraTransform.position; // Set the last camera position to the current position of the camera
    }
}
