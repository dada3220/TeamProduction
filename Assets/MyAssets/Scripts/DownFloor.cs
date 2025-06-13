using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class DownFloor : MonoBehaviour
{
    public float fallDelay = 0.1f;        // ����ł��痎����܂ł̕b��
    public float resetDelay = 5f;       // �����Ă��畜���܂ł̕b��

    private Rigidbody2D rb;
    private Collider2D col;
    private Vector3 initialPosition;
    private Quaternion initialRotation;

    private bool isFalling = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        initialPosition = transform.position;
        initialRotation = transform.rotation;

        rb.bodyType = RigidbodyType2D.Kinematic;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isFalling && collision.collider.CompareTag("Player"))
        {
            isFalling = true;
            StartCoroutine(FallAndReset());
        }
    }

    private IEnumerator FallAndReset()
    {
        yield return new WaitForSeconds(fallDelay);

        // �����J�n
        rb.bodyType = RigidbodyType2D.Dynamic;
        col.enabled = false; // �Փ˂𖳌��ɂ��Ď��R�������ۂ�������i�C�Ӂj

        yield return new WaitForSeconds(resetDelay);

        // ��������
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;

        transform.position = initialPosition;
        transform.rotation = initialRotation;
        col.enabled = true;

        isFalling = false;
    }
}
