using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 12f;
    public float jumpForce = 15f;
    public float groundCheckRadius = 0.5f;
    public Transform groundCheck;
    public LayerMask groundLayer;
    
    private bool isGrounded;
    private bool isJumping;
    private Rigidbody2D rb;
    private int moveDirection = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        
        if (isGrounded && isJumping)
        {
            isJumping = false;
        }
    }

    void FixedUpdate()
    {
        float targetVelocity = moveDirection * moveSpeed;
        rb.linearVelocity = new Vector2(targetVelocity, rb.linearVelocity.y);
    }

    public void MoveLeft()
    {
        moveDirection = -1;
        transform.localScale = new Vector3(-1, 1, 1);
    }

    public void MoveRight()
    {
        moveDirection = 1;
        transform.localScale = new Vector3(1, 1, 1);
    }

    public void Stop()
    {
        moveDirection = 0;
    }

    public void Jump()
    {
        if (isGrounded && !isJumping)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isJumping = true;
        }
    }

    void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}