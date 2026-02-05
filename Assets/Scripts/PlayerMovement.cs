using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 7f;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundRadius = 0.2f;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private float mobileMoveDirection = 0f;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // 1. Check for Ground
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayer);

        // 2. Handle Jump (Works for Spacebar)
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }
    }

    void FixedUpdate()
    {
        // 3. Combine Inputs
        // We take the keyboard input AND the mobile button input. 
        // If keyboard is 0, mobile takes over. If mobile is 0, keyboard takes over.
        float keyboardInput = Input.GetAxis("Horizontal");

        // We use Mathf.Clamp to ensure we don't go "double speed" if pressing both
        float combinedInput = Mathf.Clamp(keyboardInput + mobileMoveDirection, -1f, 1f);

        rb.linearVelocity = new Vector2(combinedInput * moveSpeed, rb.linearVelocity.y);
    }

    // --- PUBLIC METHODS FOR UI BUTTONS ---

    public void MoveLeft()
    {
        mobileMoveDirection = -1f;
    }

    public void MoveRight()
    {
        mobileMoveDirection = 1f;
    }

    public void StopMoving()
    {
        mobileMoveDirection = 0f;
    }

    public void Jump()
    {
        if (isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }
}