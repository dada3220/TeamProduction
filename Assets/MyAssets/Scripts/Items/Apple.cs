using UnityEngine;

public class Apple : MonoBehaviour
{
    public int scoreValue = 10;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // スコアを加算
            GameManager.Instance.AddScore(scoreValue);
            

            Debug.Log("score : 10");
            // アイテムを消す
            Destroy(gameObject);
        }
    }
}
