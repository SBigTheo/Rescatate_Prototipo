using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerSoundController playerSoundController;
    public float moveSpeed = 12f;
    public float jumpForce = 15f;
    float timeByStep = 0.3f;
    float cont = 0f;
    bool step1 = false;

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

        ManejarPasos();
    }

    void FixedUpdate()
    {
        float targetVelocity = moveDirection * moveSpeed;
        rb.linearVelocity = new Vector2(targetVelocity, rb.linearVelocity.y);
    }

    void ManejarPasos() 
    {
        if (moveDirection == 0 || !isGrounded)
        {
            cont = timeByStep;
            return;
        }

        cont += Time.deltaTime;
        if (cont >= timeByStep) 
        {
            cont = 0f;
            if (step1) playerSoundController.playPaso1();
            else playerSoundController.playPaso2();
            step1 = !step1;
        }
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
            playerSoundController.playSalto();
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