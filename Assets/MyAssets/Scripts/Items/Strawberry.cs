using UnityEngine;

public class Strawberry : MonoBehaviour
{
    public int scoreValue = 100;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // スコアを加算
            GameManager.Instance.AddScore(scoreValue);

            Debug.Log("いちごを手に入れた");
            // アイテムを消す
            Destroy(gameObject);
        }
    }
}
