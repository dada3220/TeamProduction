using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject[] stagePrefabs; // プレハブ登録

    private GameObject currentStage;
    private GameObject currentPlayer;
    private int stageIndex = 0;

    private void Start()
    {
        LoadStage(stageIndex);
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

