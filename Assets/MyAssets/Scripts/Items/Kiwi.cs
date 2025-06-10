using UnityEngine;

public class Kiwi : MonoBehaviour
{
    public int scoreValue = 60;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // スコアを加算
            GameManager.Instance.AddScore(scoreValue);

            Debug.Log("キウイを手に入れた");
            // アイテムを消す
            Destroy(gameObject);
        }
    }
}
