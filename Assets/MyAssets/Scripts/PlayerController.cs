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

    private bool isInvincible = false;       // 無敵状態フラグ
    public float invincibleTime;        // 無敵時間
    public float knockbackForce;        // ノックバックの強さ

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

        // 色変更で無敵演出
        GetComponent<SpriteRenderer>().color = Color.red;

        yield return new WaitForSeconds(0.2f); // ちょっとだけノックバックの移動

        // ノックバックを止める（または緩やかに減衰させる）
        rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);

        yield return new WaitForSeconds(0.8f); // 操作不能の残り時間
        canMove = true;

        yield return new WaitForSeconds(invincibleTime - 1.0f); // 無敵継続
        isInvincible = false;
        GetComponent<SpriteRenderer>().color = Color.white;
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
            // 着地直後なら速度を０にする(摩擦０で設定しているため滑り防止)
            rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);

            animator.SetTrigger("Walk");
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

            // ノックバック処理（Obstacleの位置から押し返す）
            Vector2 knockbackDir = 
                (transform.position - collision.transform.position).normalized;
            rb.linearVelocity = 
                new Vector2(knockbackDir.x * knockbackForce, knockbackForce * 2.0f);
        }
    }
}
