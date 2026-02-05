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
        // Ground Detection logic
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, stats.groundRadius, groundLayer);

        // Keyboard Jump
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        Debug.Log("Is Grounded: " + isGrounded);
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
        // Merge Keyboard (Horizontal) and Mobile (Buttons)
        float keyboardInput = Input.GetAxisRaw("Horizontal");
        float combinedInput = Mathf.Clamp(keyboardInput + mobileMoveDirection, -1f, 1f);

        rb.linearVelocity = new Vector2(combinedInput * stats.moveSpeed, rb.linearVelocity.y);
    }

    

    // --- PUBLIC METHODS FOR UI BUTTONS ---

    public void Jump()
    {
        if (isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, stats.jumpForce);
        }
    }

    public void MoveLeft() => mobileMoveDirection = -1f;
    public void MoveRight() => mobileMoveDirection = 1f;
    public void StopMoving() => mobileMoveDirection = 0f;
}