using UnityEngine;
using UnityEngine.SceneManagement;

public class ContinueButton : MonoBehaviour
{
    public GameObject gameOverUI;
   

    public void OnContinueButtonClicked()
    {
        
        SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
        
    }
}
