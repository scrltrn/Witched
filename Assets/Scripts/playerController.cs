using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : NetworkBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    private Vector2 _moveInput;

    [Header("Ground Check Settings")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;
    private bool _isGrounded;

    private Animator _animator;
    private Rigidbody2D _rb;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
    }

    public void OnMove(InputValue value)
    {
        if (!IsOwner) return;
        _moveInput = value.Get<Vector2>();
    }

    // The New Input System automatically calls this when you press your Jump button!
    public void OnJump(InputValue value)
    {
        if (!IsOwner) return;

        // Only jump if the button was pressed AND the character is touching the ground
        if (value.isPressed && _isGrounded)
        {
            // Reset the Y velocity first so falling momentum doesn't fight the jump
            _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, 0);

            // Apply a sudden upward burst of force
            _rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    void FixedUpdate()
    {
        if (!IsOwner) return;

        // 1. Draw an invisible circle at the character's feet to check for the Ground layer
        if (groundCheck != null)
        {
            _isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        }

        // 2. Handle left/right movement (gravity still handles the Y axis smoothly!)
        _rb.linearVelocity = new Vector2(_moveInput.x * moveSpeed, _rb.linearVelocity.y);
    }

    void Update()
    {
        if (!IsOwner) return;

        if (_animator != null)
        {
            bool isMoving = Mathf.Abs(_moveInput.x) > 0.1f;
            _animator.SetBool("isRunning", isMoving);
        }
    }
}