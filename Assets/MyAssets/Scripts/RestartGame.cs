using UnityEngine;

public class RestartGame : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.RestartGame();
            BGMManager.Instance?.PlayBGM(1);// BGMçƒê∂
        }
    }

}
