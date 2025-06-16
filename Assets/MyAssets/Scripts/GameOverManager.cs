using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public static GameOverManager Instance;

    
    private void Awake()
    {
        // シングルトン（1つだけ存在）
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // シーンをまたいでも維持
        }
        else
        {
            Destroy(gameObject);
        }
    }
    

    public void GameOver(GameObject player)
    {
        if (player != null)
        {
            Destroy(player);
        }

        BGMManager.Instance.StopBGM(); //BGM停止
        SEManager.Instance.PlaySE(1); //ゲームオーバーSE再生
        SceneManager.LoadScene("GameOverScene");
    }

}
