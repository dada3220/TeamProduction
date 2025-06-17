using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class GameManager : SingletonMonoBehaviour<GameManager>
{
    //public static GameManager Instance;

    public TMP_Text scoreText; // ScoreのUI

    
    public GameObject playerPrefab;
    public GameObject[] stagePrefabs; // ステージプレハブの配列
    

    private GameObject currentStage;
    private GameObject currentPlayer;

   

    private int stageIndex = 0;
    private int score = 0;

    public int Score
    {
        get { return score; }
        private set { score = value; }
    }

    


    private void Start()
    {
        if(GameManager.Instance != null)
        {
            BGMManager.Instance?.PlayBGM(1);// BGM再生
            RestartGame();
        }
       
        LoadStage(stageIndex);
        UpdateScoreUI();
       
    }

   



    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreUI();
    }
    private void UpdateScoreUI()
    {
        if(scoreText == null)
        {
            GameObject obj = GameObject.Find("Score");

            if(obj != null)
            {
                scoreText = obj.GetComponent<TMP_Text>();
            }
            else
            {
                Debug.Log("スコアオブジェクトが見つかりません");
            }
        }


        if(scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
        

        
    }

    public void LoadStage(int index)
    {
        if (currentStage != null) Destroy(currentStage);
        if (currentPlayer != null) Destroy(currentPlayer);

        // 現在のステージ配列から参照
        currentStage = Instantiate(stagePrefabs[index]);

        // StartPoint
        Transform startPoint = currentStage.transform.Find("StartPoint");
        if(startPoint == null)
        {
            Debug.LogError("StartPointが見つかりませんでした");
            return;
        }
        
        currentPlayer = Instantiate(playerPrefab, startPoint.position, Quaternion.identity);
    }

    public void LoadNextStage()
    {
        stageIndex++;
        if(stageIndex < stagePrefabs.Length)
        {
            LoadStage(stageIndex);
        }
        else
        {
            BGMManager.Instance.StopBGM();
            SceneManager.LoadScene("Clear", LoadSceneMode.Single);
            Debug.Log("全ステージクリア");
        }
    }

    public void RestartGame()
    {
        stageIndex = 0;
        score = 0;
        
        if (currentStage != null) Destroy(currentStage);
        if (currentPlayer != null) Destroy(currentPlayer); 

        LoadStage(stageIndex);
        UpdateScoreUI();

    }

    
}

