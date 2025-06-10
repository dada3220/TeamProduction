using UnityEngine;

public class Strawberry : MonoBehaviour
{
    public int scoreValue = 100;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // �X�R�A�����Z
            GameManager.Instance.AddScore(scoreValue);

            Debug.Log("����������ɓ��ꂽ");
            // �A�C�e��������
            Destroy(gameObject);
        }
    }
}
