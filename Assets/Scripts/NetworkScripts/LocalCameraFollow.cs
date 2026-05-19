using System.Globalization;
using Unity.Netcode;
using UnityEngine;

public class LocalCameraFollow : NetworkBehaviour
{
    private Camera mainCamera;
    void Start()
    {
        // Find the main camera in the scene when the player spawns
        mainCamera = Camera.main;
    }

    void LateUpdate()
    {
        // IsOwner checks if THIS specific character belongs to your computer.
        // If it belongs to your friend over the network, this code does nothing.
        if (IsOwner && mainCamera != null)
        {
            // Get the player's current position
            Vector3 newCameraPosition = transform.position;

            // Keep the camera pushed back on the Z axis (usually -10 in 2D) so it can actually see you
            newCameraPosition.z = mainCamera.transform.position.z;

            // Move the camera to the player's position
            mainCamera.transform.position = newCameraPosition;
        }
    }
}
