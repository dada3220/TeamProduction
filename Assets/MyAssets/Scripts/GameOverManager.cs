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

        BGMManager.Instance.StopBGM(); //BGM��~
        SEManager.Instance.PlaySE(1); //�Q�[���I�[�o�[SE�Đ�
        SceneManager.LoadScene("GameOverScene");
    }

}
