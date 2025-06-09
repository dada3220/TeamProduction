using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;   // ���E�̈ړ��X�s�[�h
    public float jumpForce;  // �W�����v�̗�

    private Rigidbody2D rb;        

    public LifeManager lifeManager; // ���C�t�}�l�[�W���[�i�C���X�y�N�^�[�Őݒ�j

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // ���C�t�}�l�[�W���[���ݒ肳��Ă��邩�m�F
        if (lifeManager == null)
        {
            Debug.LogError("LifeManager���ݒ肳��Ă��܂���");
        }
    }

    void Update()
    {
        Move();         // �ړ�����
        Jump();         // �W�����v����
    }

    // ���E�ړ��̏���
    void Move()
    {
        float moveX = Input.GetAxisRaw("Horizontal"); // ��: -1�A�E: 1
        rb.linearVelocity = new Vector2(moveX * moveSpeed, rb.linearVelocity.y);
    }

    // �W�����v����
    void Jump()
    {
        // ���L�[�������Ă��āA���n�ʂɂ���Ƃ������W�����v
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }

    // �����ƏՓ˂����Ƃ��̏���
    void OnCollisionEnter2D(Collision2D collision)
    {
        // ��Q���ɂԂ������烉�C�t�����炷
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            lifeManager.TakeDamage(1); // ���C�t�}�l�[�W���[��ʂ��ă��C�t�����炷
        }
    }
}
