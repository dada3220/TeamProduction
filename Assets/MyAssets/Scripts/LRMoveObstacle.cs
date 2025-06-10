using UnityEngine;
using Cysharp.Threading.Tasks;      
using System.Threading;             // CancellationToken用

// 障害物の移動を制御するクラス（左右に往復するタイプ）
public class LRMoveObstacle : ObstacleBase
{
    public float distance = 2f;     // 移動距離（右方向にどれだけ動くか）
    public float duration = 1f;     // 片道の移動にかかる時間（秒）

    // ObstacleBase で定義された抽象メソッドを実装
    protected override async UniTask StartMovementAsync(CancellationToken token)
    {
        // 初期位置を取得
        Vector3 start = transform.position;
        // 移動先の位置
        Vector3 end = start + Vector3.right * distance;

        // キャンセルされるまで左右に往復
        while (!token.IsCancellationRequested)
        {
            // 右へ移動
            await MoveOverTime(start, end, duration, token);
            // 左へ移動
            await MoveOverTime(end, start, duration, token);
        }
    }

    // 指定された時間で移動する非同期メソッド
    private async UniTask MoveOverTime
        (Vector3 from, Vector3 to, float time, CancellationToken token)
    {
        float elapsed = 0f;

        // 経過時間が目的の移動時間に達するまで繰り返す
        while (elapsed < time)
        {
            // もしオブジェクトが削除されたなどでキャンセルされたら中断
            if (token.IsCancellationRequested) return;

            // 時間を加算
            elapsed += Time.deltaTime;

            // t = 0.0～1.0 の範囲に補正
            float t = Mathf.Clamp01(elapsed / time);

            // 線形補間（Lerp）で位置を計算
            transform.position = Vector3.Lerp(from, to, t);

            // 1フレーム待機（キャンセル対応）
            await UniTask.Yield(PlayerLoopTiming.Update, token);
        }

        transform.position = to;
    }
}
