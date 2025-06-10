using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;   // 左右の移動スピード
    public float jumpForce;  // ジャンプの力

    private Rigidbody2D rb;        

    public LifeManager lifeManager; // ライフマネージャー（インスペクターで設定）

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // ライフマネージャーが設定されているか確認
        if (lifeManager == null)
        {
            Debug.LogError("LifeManagerが設定されていません");
        }
    }

    void Update()
    {
        Move();         // 移動処理
        Jump();         // ジャンプ処理
    }

    // 左右移動の処理
    void Move()
    {
        float moveX = Input.GetAxisRaw("Horizontal"); // 左: -1、右: 1
        rb.linearVelocity = new Vector2(moveX * moveSpeed, rb.linearVelocity.y);
    }

    // ジャンプ処理
    void Jump()
    {
        // ↑キーを押していて、かつ地面にいるときだけジャンプ
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }

    // 何かと衝突したときの処理
    void OnCollisionEnter2D(Collision2D collision)
    {
        // 障害物にぶつかったらライフを減らす
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            lifeManager.TakeDamage(1); // ライフマネージャーを通じてライフを減らす
        }
    }
}
