using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;

    private Animator animator;
    private Rigidbody2D rb;
    private LifeManager lifeManager;

    private bool isGrounded;
    private int jumpCount = 0;
    private int maxJumpCount = 1;

    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    private bool isInvincible = false;
    public float invincibleTime;
    public float knockbackForce;

    private bool canMove = true;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        if (lifeManager == null)
        {
            lifeManager = FindFirstObjectByType<LifeManager>();
            if (lifeManager == null)
            {
                Debug.LogError("LifeManagerが見つかりません");
            }
        }
    }

    void Update()
    {
        CheckGround();
        Move();
        Jump();

        float moveX = Input.GetAxisRaw("Horizontal");
        animator.SetFloat("Speed", Mathf.Abs(moveX));
        animator.SetBool("IsGrounded", isGrounded);
    }

    void Move()
    {
        if (!canMove) return;

        float moveX = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(moveX * moveSpeed, rb.linearVelocity.y);

        // 左右反転
        if (moveX != 0)
        {
            GetComponent<SpriteRenderer>().flipX = moveX < 0;
        }
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && jumpCount < maxJumpCount)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            jumpCount++;
            animator.SetTrigger("Jump");
        }
    }

    void CheckGround()
    {
        bool wasGrounded = isGrounded;
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if (isGrounded && !wasGrounded)
        {
            // 着地直後に速度リセット
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

            // ノックバック
            Vector2 knockbackDir = (transform.position - collision.transform.position).normalized;
            rb.linearVelocity = new Vector2(knockbackDir.x * knockbackForce, knockbackForce * 2.0f);
        }
    }

    IEnumerator InvincibilityCoroutine()
    {
        isInvincible = true;
        canMove = false;

        GetComponent<SpriteRenderer>().color = Color.red;

        yield return new WaitForSeconds(0.2f);

        rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);

        yield return new WaitForSeconds(0.8f);
        canMove = true;

        yield return new WaitForSeconds(invincibleTime - 1.0f);
        isInvincible = false;
        GetComponent<SpriteRenderer>().color = Color.white;
    }
}
