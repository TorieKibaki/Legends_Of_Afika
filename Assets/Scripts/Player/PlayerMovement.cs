using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Data Source")]
    public PlayerStats stats; // Drag your 'MainPlayerStats' file here

    [Header("Environment")]
    public Transform groundCheck;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private float mobileMoveDirection = 0f;
    private bool isGrounded;

    // Assists
    private float coyoteTimeCounter;
    private float jumpBufferCounter;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (stats == null)
        {
            Debug.LogError("Please assign a PlayerStats Scriptable Object to the PlayerMovement script!");
        }
    }

    void Update()
    {
        // Block Input if UI is active
        if (UIManager.instance != null && UIManager.instance.IsInputLocked) return;

        // Ground Detection & Coyote Time
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, stats.groundRadius, groundLayer);

        if (isGrounded)
        {
            coyoteTimeCounter = stats.coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        // Jump Buffering
        if (Input.GetButtonDown("Jump"))
        {
            jumpBufferCounter = stats.jumpBufferTime;
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }

        // Jump Execution
        if (coyoteTimeCounter > 0f && jumpBufferCounter > 0f)
        {
            PerformJump();
        }

        // Cut Jump Height on Release (Variable Jump Height)
        if (Input.GetButtonUp("Jump") && rb.linearVelocity.y > 0f)
        {
            coyoteTimeCounter = 0f;
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null && stats != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, stats.groundRadius);
        }
    }

    void FixedUpdate()
    {
        if (UIManager.instance != null && UIManager.instance.IsInputLocked)
        {
            // Stop horizontal movement, preserve gravity
            rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);
            return;
        }

        // Merge Keyboard (Horizontal) and Mobile (Buttons)
        float keyboardInput = Input.GetAxisRaw("Horizontal");
        float combinedInput = Mathf.Clamp(keyboardInput + mobileMoveDirection, -1f, 1f);

        // Apply Horizontal Velocity (Keep vertical `y` for jump)
        rb.linearVelocity = new Vector2(combinedInput * stats.moveSpeed, rb.linearVelocity.y);

        // --- BETTER JUMP PHYSICS ---
        // 1. Falling? falling faster (Heavier feel)
        if (rb.linearVelocity.y < 0)
        {
            rb.gravityScale = stats.fallMultiplier;
        }
        // 2. Jumping up but NOT holding space? short hop
        // Note: checking 'Jump' button for variable height. 
        // For mobile, you might need a separate 'isHoldingJump' flag if using on-screen buttons.
        else if (rb.linearVelocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.gravityScale = stats.lowJumpMultiplier;
        }
        // 3. Normal gravity otherwise
        else
        {
            rb.gravityScale = 1f;
        }
    }

    private void PerformJump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, stats.jumpForce);
        
        // Reset counters to prevent double jumps / spam
        jumpBufferCounter = 0f;
        coyoteTimeCounter = 0f;
    }

    // --- PUBLIC METHODS FOR UI BUTTONS ---

    public void Jump()
    {
        // For mobile button: simply set the buffer
        // This will be picked up in the next Update() and executed if grounded (or coyote)
        jumpBufferCounter = stats.jumpBufferTime;
    }

    public void MoveLeft() => mobileMoveDirection = -1f;
    public void MoveRight() => mobileMoveDirection = 1f;
    public void StopMoving() => mobileMoveDirection = 0f;
}