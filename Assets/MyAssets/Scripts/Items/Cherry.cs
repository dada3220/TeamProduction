using UnityEngine;

public class Cherry : MonoBehaviour
{
    public int scoreValue = 40;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // スコアを加算
            GameManager.Instance.AddScore(scoreValue);

            Debug.Log("チェリーを手に入れた");
            // アイテムを消す
            Destroy(gameObject);
        }
    }
}
