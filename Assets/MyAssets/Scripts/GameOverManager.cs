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

        SceneManager.LoadScene("GameOverScene");
    }

}
