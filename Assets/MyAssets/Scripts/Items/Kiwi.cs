using UnityEngine;

public class Kiwi : MonoBehaviour
{
    public int scoreValue = 60;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // �X�R�A�����Z
            GameManager.Instance.AddScore(scoreValue);

            Debug.Log("�L�E�C����ɓ��ꂽ");
            // �A�C�e��������
            Destroy(gameObject);
        }
    }
}
