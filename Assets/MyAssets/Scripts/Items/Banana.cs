using UnityEngine;

public class Banana : MonoBehaviour
{
    public int scoreValue = 20;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // スコアを加算
            GameManager.Instance.AddScore(scoreValue);

            Debug.Log("バナナを手に入れた");
            // アイテムを消す
            Destroy(gameObject);
        }
    }
}
