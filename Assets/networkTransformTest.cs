using Unity.Netcode;
using UnityEngine;

public class NetworkTransformTest : NetworkBehaviour
{
    // You can change this in the Unity Inspector
    public float radius = 3f;

    void Update()
    {
        if (IsServer)
        {
            float theta = Time.time;

            // Cleaned up with Vector2, Mathf, and X/Y axes for 2D
            transform.position = new Vector2(Mathf.Cos(theta) * radius, Mathf.Sin(theta) * radius);
        }
    }
}