using UnityEngine;


public class GoalPoint : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("ゴール！");

            // 例：次のシーンを読み込む（名前または番号）
            // SceneManager.LoadScene("NextStage");
            // または：
            gameManager.LoadNextStage();

            // 今はゴール時にDebugログを出すだけ
        }
    }
}
