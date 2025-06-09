using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;

// 上下に往復する障害物クラス
public class UDMoveObstacle : ObstacleBase
{
    public float distance = 2f;   // 上方向に移動する距離
    public float duration = 1f;   // 片道移動の時間（秒）

    // 障害物の移動ロジック（上下往復）
    protected override async UniTask StartMovementAsync(CancellationToken token)
    {
        // 現在の位置を取得（開始位置）
        Vector3 start = transform.position;
        // 上方向への移動先を計算（上が +Y）
        Vector3 end = start + Vector3.up * distance;

        // キャンセルされるまで上下に往復し続ける
        while (!token.IsCancellationRequested)
        {
            // 上へ移動
            await MoveOverTime(start, end, duration, token);
            // 下へ戻る
            await MoveOverTime(end, start, duration, token);
        }
    }

    // 指定時間で from → to に移動する補間処理
    private async UniTask MoveOverTime(Vector3 from, Vector3 to, float time, CancellationToken token)
    {
        float elapsed = 0f;

        while (elapsed < time)
        {
            if (token.IsCancellationRequested) return;

            elapsed += Time.deltaTime;

            float t = Mathf.Clamp01(elapsed / time);

            transform.position = Vector3.Lerp(from, to, t);

            await UniTask.Yield(PlayerLoopTiming.Update, token);
        }

        transform.position = to;
    }
}
