using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public static GameOverManager Instance;

    
    private void Awake()
    {
        // �V���O���g���i1�������݁j
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // �V�[�����܂����ł��ێ�
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

        BGMManager.Instance.StopBGM(); //BGM��~
        SEManager.Instance.PlaySE(1); //�Q�[���I�[�o�[SE�Đ�
        SceneManager.LoadScene("GameOverScene");
    }

}
