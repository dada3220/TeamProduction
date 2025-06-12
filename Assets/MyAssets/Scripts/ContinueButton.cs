using UnityEngine;
using UnityEngine.SceneManagement;

public class ContinueButton : MonoBehaviour
{
    public void OnContinueButtonClicked()
    {

        SceneManager.LoadScene("GameScene",LoadSceneMode.Single);
        GameManager.Instance.RestartGame();
        
    }
}
