using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Text scoreText; // UIのText

    public GameObject playerPrefab;
    public GameObject[] stagePrefabs; // プレハブ登録

    private GameObject currentStage;
    private GameObject currentPlayer;
    private int stageIndex = 0;
    private int score = 0;
    private void Awake()
    {
        // シングルトンの初期化
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // シーンをまたいでも残す場合
        }
        else
        {
            Destroy(gameObject); // 2つ目以降は削除
            return;
        }
    }


    private void Start()
    {
        LoadStage(stageIndex);
        UpdateScoreUI();
    }

    public void AddScore(int amount)
    {
        score += amount;
    }
    private void UpdateScoreUI()
    {
        if(scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }

    public void LoadStage(int index)
    {
        if (currentStage != null) Destroy(currentStage);
        if (currentPlayer != null) Destroy(currentPlayer);

        // ステージプレハブを生成
        currentStage = Instantiate(stagePrefabs[index]);

        // StartPoint を探す
        Transform startPoint = currentStage.transform.Find("StartPoint");
        if(startPoint == null)
        {
            Debug.LogError("StartPointがステージプレハブに見つかりません");
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
            Debug.Log("全ステージクリア");
        }
    }
}

