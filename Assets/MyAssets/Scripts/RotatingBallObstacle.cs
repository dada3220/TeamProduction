using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;

/// <summary>
/// Pivot を中心に鉄球 (Ball) を radius で回転させ、
/// Chain を常に Pivot→Ball 方向へ向ける
/// </summary>
public class RotatingBallObstacle : ObstacleBase
{
    [Header("必須参照")]
    public Transform pivot;      // 回転中心
    public Transform ball;       // 鉄球
    public Transform chain;      // チェーン（SpriteRenderer を想定）

    [Header("パラメータ")]
    public float radius = 2f;        // 回転半径
    public float angularSpeed = 90f; // 度/秒 (正: 反時計回り)

    private float angleDeg;          // 現在角度[deg]

    protected override async UniTask StartMovementAsync(CancellationToken token)
    {
        // 初期角度を 0 に
        angleDeg = 0f;

        // チェーン長を半径に合わせてスケール調整（Sprite の幅=1想定）
        if (chain != null)
        {
            Vector3 s = chain.localScale;
            chain.localScale = new Vector3(radius, s.y, s.z);
        }

        while (!token.IsCancellationRequested)
        {
            // 角度更新
            angleDeg += angularSpeed * Time.deltaTime;
            if (angleDeg >= 360f) angleDeg -= 360f;
            if (angleDeg <= -360f) angleDeg += 360f;

            // 位置を計算 (XY 平面)
            float rad = angleDeg * Mathf.Deg2Rad;
            Vector3 offset = new Vector3(Mathf.Cos(rad), Mathf.Sin(rad), 0f) * radius;
            ball.position = pivot.position + offset;

            // チェーンを Pivot→Ball 方向へ向ける
            if (chain != null)
            {
                Vector2 dir = ball.position - pivot.position;
                float zRot = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                chain.rotation = Quaternion.AngleAxis(zRot, Vector3.forward);
            }

            await UniTask.Yield(PlayerLoopTiming.Update, token);
        }
    }
}
