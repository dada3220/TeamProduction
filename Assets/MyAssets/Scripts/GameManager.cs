using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private void Awake()
    {
        // シングルトン化
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // シーンをまたいで保持
        }
        else
        {
            Destroy(gameObject); // 2つ目以降は削除
        }
    }

    // シーン読み込み
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // 現在のシーンをリロード
    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // 次のシーンへ（Build Index順）
    public void LoadNextScene()
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentIndex + 1);
    }
}

