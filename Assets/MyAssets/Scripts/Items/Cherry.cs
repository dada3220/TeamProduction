using UnityEngine;

public class Cherry : MonoBehaviour
{
    public int scoreValue = 40;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // �X�R�A�����Z
            GameManager.Instance.AddScore(scoreValue);

            Debug.Log("�`�F���[����ɓ��ꂽ");
            // �A�C�e��������
            Destroy(gameObject);
        }
    }
}
