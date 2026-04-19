using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : NetworkBehaviour
{
    public float moveSpeed = 5f;
    private Vector2 _moveInput;

    // Reference to the Animator component
    private Animator _animator;

    void Start()
    {
        // Find the Animator on this prefab when it spawns
        _animator = GetComponent<Animator>();
    }

    public void OnMove(InputValue value)
    {
        if (!IsOwner) return;
        _moveInput = value.Get<Vector2>();
    }

    void Update()
    {
        if (!IsOwner) return;

        // Movement Logic
        Vector3 movement = new Vector3(_moveInput.x, _moveInput.y, 0);
        transform.position += movement * moveSpeed * Time.deltaTime;

        // Animation Logic
        if (_animator != null)
        {
            // Check if the player is currently pushing any keys
            bool isMoving = _moveInput.sqrMagnitude > 0.1f;

            // This assumes you have a Bool parameter in your Animator named "IsWalking"
            _animator.SetBool("IsWalking", isMoving);
        }
    }
}