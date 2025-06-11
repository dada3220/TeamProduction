using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;

    private Rigidbody2D rb;
    private LifeManager lifeManager;

    private bool isGrounded;
    private int jumpCount = 0;
    private int maxJumpCount = 1;

    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    private bool isInvincible = false;       // –³“Gó‘Ôƒtƒ‰ƒO
    public float invincibleTime;        // –³“GŠÔ
    public float knockbackForce;        // ƒmƒbƒNƒoƒbƒN‚Ì‹­‚³

    private bool canMove = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (lifeManager == null)
        {
            lifeManager = FindFirstObjectByType<LifeManager>();
            if (lifeManager == null)
            {
                Debug.LogError("LifeManager‚ªŒ©‚Â‚©‚è‚Ü‚¹‚ñ");
            }
        }
    }

    void Update()
    {
        CheckGround();
        Move();
        Jump();
    }

 
    void Move()
    {
        if (!canMove) return;
        float moveX = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(moveX * moveSpeed, rb.linearVelocity.y);
    }

    IEnumerator InvincibilityCoroutine()
    {
        isInvincible = true;
        canMove = false;

        //–³“G’†F•Ï‰»
        GetComponent<SpriteRenderer>().color = Color.red;

        yield return new WaitForSeconds(1.0f); // 0.5•b‚¾‚¯‘€ì’â~
        canMove = true;

        yield return new WaitForSeconds(invincibleTime - 1.0f); // c‚è–³“GŠÔ

        isInvincible = false;
        GetComponent<SpriteRenderer>().color = Color.white;
    }


    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && jumpCount < maxJumpCount)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            jumpCount++;
        }
    }

    void CheckGround()
    {
        bool wasGrounded = isGrounded;
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if (isGrounded && !wasGrounded)
        {
            // ’…’n’¼Œã‚È‚ç‘¬“x‚ğ‚O‚É‚·‚é(–€C‚O‚Åİ’è‚µ‚Ä‚¢‚é‚½‚ßŠŠ‚è–h~)
            rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);

        }

        if (isGrounded)
        {
            jumpCount = 0;
        }
    }

void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle") && !isInvincible)
        {
            lifeManager.TakeDamage(1);
            StartCoroutine(InvincibilityCoroutine());

            // ƒmƒbƒNƒoƒbƒNˆ—iObstacle‚ÌˆÊ’u‚©‚ç‰Ÿ‚µ•Ô‚·j
            Vector2 knockbackDir = 
                (transform.position - collision.transform.position).normalized;
            rb.linearVelocity = 
                new Vector2(knockbackDir.x * knockbackForce, knockbackForce * 2.0f);
        }
    }
}
