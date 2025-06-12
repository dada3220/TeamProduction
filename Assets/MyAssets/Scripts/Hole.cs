using UnityEngine;

public class Hole : MonoBehaviour
{
   

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            GameOverManager.Instance.GameOver(collision.gameObject);
            Debug.Log("ゲームオーバー処理を行う");
        }
    }
}
