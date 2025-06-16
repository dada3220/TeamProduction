using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleController : MonoBehaviour
{
    public void OnStartButtonClicked()
    {
        SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
    }

    public void OnTitleButtonClicked()
    {
        SceneManager.LoadScene("Title", LoadSceneMode.Single);
    }

    public void OnQuitButtonClicked()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;//ゲームプレイ終了
#else
    Application.Quit();//ゲームプレイ終了
#endif
    }
}
