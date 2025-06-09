using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [Header("プレイヤーの参照")]
    public GameObject playerPrefab;


    void Start()
    {
        Transform startPoint = GameObject.Find("StartPoint")?.transform;


        if (playerPrefab != null && startPoint != null)
        {
            Instantiate(playerPrefab, startPoint.position, Quaternion.identity);
        }
        else
        {
            Debug.LogError("StartPoint または playerPrefab が見つかりません！");
        }
    }
}
