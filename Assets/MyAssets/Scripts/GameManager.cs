using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake()
    {
        // �V���O���g����
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // �V�[�����܂����ŕێ�
        }
        else
        {
            Destroy(gameObject); // 2�ڈȍ~�͍폜
        }
    }

    // �V�[���ǂݍ���
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // ���݂̃V�[���������[�h
    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // ���̃V�[���ցiBuild Index���j
    public void LoadNextScene()
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentIndex + 1);
    }
}

