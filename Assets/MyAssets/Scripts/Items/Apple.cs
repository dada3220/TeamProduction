using UnityEngine;

public class Apple : MonoBehaviour
{
    public int scoreValue = 10;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // �X�R�A�����Z
            GameManager.Instance.AddScore(scoreValue);
            

            Debug.Log("score : 10");
            // �A�C�e��������
            Destroy(gameObject);
        }
    }
}
