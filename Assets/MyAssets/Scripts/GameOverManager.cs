using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : SingletonMonoBehaviour<GameOverManager>
{
    

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
