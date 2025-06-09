using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [Header("プレイヤーとスタート地点の参照")]
    public GameObject playerPrefab;
    public Transform startPoint;
    void Start()
    {

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
