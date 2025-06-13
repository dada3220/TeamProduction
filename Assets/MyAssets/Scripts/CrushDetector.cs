using UnityEngine;

public class CrushDetectorByRay : MonoBehaviour
{
    public float checkDistance = 0.1f;           // 上下チェックの距離
    public LayerMask crushLayer;                 // Stone や Ground のレイヤーを指定
    public float requiredCrushTime = 0.05f;       // どれくらい挟まれていたら即死するか
    private float crushTimer = 0f;

    void Update()
    {
        bool isCrushedFromTop = Physics2D.Raycast(transform.position, Vector2.up, checkDistance, crushLayer);
        bool isCrushedFromBottom = Physics2D.Raycast(transform.position, Vector2.down, checkDistance, crushLayer);

        if (isCrushedFromTop && isCrushedFromBottom)
        {
            crushTimer += Time.deltaTime;

            if (crushTimer >= requiredCrushTime)
            {
                if (GameOverManager.Instance != null)
                    GameOverManager.Instance.GameOver(gameObject);
            }
        }
        else
        {
            crushTimer = 0f; // 条件から外れたらタイマーリセット
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Debug表示：Raycast線
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.up * checkDistance);
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * checkDistance);
    }
}
