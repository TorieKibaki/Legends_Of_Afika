using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Data Source")]
    public PlayerStats stats; // Drag your 'MainPlayerStats' file here

    [Header("Environment")]
    public Transform groundCheck;
    public LayerMask groundLayer;

    [Header("Animation")]
    public Animator animator;

    [Header("Audio")]
    public AudioClip jumpSound;
    public AudioClip footstepSound;
    [Range(0f, 1f)] public float footstepVolume = 0.5f;

    private AudioSource footstepSource;

    private Rigidbody2D rb;
    private float mobileMoveDirection = 0f;
    private bool isGrounded;

    // Assists
    private float coyoteTimeCounter;
    private float jumpBufferCounter;
    private bool isHoldingJump = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (stats == null)
        {
            Debug.LogError("Please assign a PlayerStats Scriptable Object to the PlayerMovement script!");
        }

        // Setup Footstep Audio Source
        footstepSource = gameObject.AddComponent<AudioSource>();
        footstepSource.clip = footstepSound;
        footstepSource.loop = true;
        footstepSource.volume = footstepVolume;
        footstepSource.playOnAwake = false;
    }

    void Update()
    {
        animator.SetFloat("Speed", Mathf.Abs(rb.linearVelocity.x));

        // Block Input if UI is active
        if (UIManager.instance != null && UIManager.instance.IsInputLocked) 
        {
            if (footstepSource.isPlaying) footstepSource.Stop();
            return;
        }

        // Ground Detection & Coyote Time
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, stats.groundRadius, groundLayer);

        animator.SetBool("IsGrounded", isGrounded);

        // --- FOOTSTEP AUDIO LOGIC ---
        if (isGrounded && Mathf.Abs(rb.linearVelocity.x) > 0.1f)
        {
            if (!footstepSource.isPlaying) footstepSource.Play();
        }
        else
        {
            if (footstepSource.isPlaying) footstepSource.Stop();
        }

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

        // Flip Character via Rotation
        if (combinedInput > 0.01f)
        {
            transform.eulerAngles = Vector3.zero;
        }
        else if (combinedInput < -0.01f)
        {
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }

        // Physics Improvements (Fall faster, low jump)
        if (rb.linearVelocity.y < 0)
        {
            rb.gravityScale = stats.fallMultiplier;
        }
        else if (rb.linearVelocity.y > 0 && !Input.GetButton("Jump") && !isHoldingJump)
        {
            rb.gravityScale = stats.lowJumpMultiplier;
        }
        else
        {
            rb.gravityScale = 1f;
        }
    }

    private void PerformJump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, stats.jumpForce);

        // Play Jump Sound
        if (AudioManager.instance != null && jumpSound != null)
        {
            AudioManager.instance.PlaySFX(jumpSound);
        }

        // Reset counters to prevent double jumps / spam
        jumpBufferCounter = 0f;
        coyoteTimeCounter = 0f;
    }

    // --- MOVING PLATFORM LOGIC ---

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // If we land on a MovingPlatform, become its child to move with it
        if (collision.gameObject.GetComponent<MovingPlatform>())
        {
            transform.SetParent(collision.transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // If we leave the platform, stop being a child
        if (collision.gameObject.GetComponent<MovingPlatform>())
        {
            transform.SetParent(null);
        }
    }

    // --- PUBLIC METHODS FOR UI BUTTONS ---

    // For EventTrigger "PointerDown" on Jump Button
    public void JumpStart()
    {
        // Set buffer to attempt jump
        jumpBufferCounter = stats.jumpBufferTime;
        isHoldingJump = true;
    }

    // For EventTrigger "PointerUp" on Jump Button
    public void JumpEnd()
    {
        isHoldingJump = false;
    }

    
    public void Jump() => JumpStart();

    public void MoveLeft() => mobileMoveDirection = -1f;
    public void MoveRight() => mobileMoveDirection = 1f;
    public void StopMoving() => mobileMoveDirection = 0f;
}