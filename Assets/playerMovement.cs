using Unity.Netcode;
using UnityEngine.InputSystem;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    private Rigidbody2D rb;

    private float Move;
    private float speed;
    void Start()
    {
        rb= GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Move = Input.GetAxisRaw("Horizontal");

        rb.linearVelocity = new Vector2(Move * speed, Move * speed);
    }
}
