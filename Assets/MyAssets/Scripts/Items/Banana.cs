using UnityEngine;

public class Banana : MonoBehaviour
{
    public int scoreValue = 20;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // �X�R�A�����Z
            GameManager.Instance.AddScore(scoreValue);

            Debug.Log("�o�i�i����ɓ��ꂽ");
            // �A�C�e��������
            Destroy(gameObject);
        }
    }
}
