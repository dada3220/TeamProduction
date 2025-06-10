using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;

// 障害物の基底クラス
public abstract class ObstacleBase : MonoBehaviour
{
    protected CancellationTokenSource cts;

    protected virtual void Start()
    {
        cts = new CancellationTokenSource();
        StartMovementAsync(cts.Token).Forget();
    }

    // 派生クラスで動作内容を実装
    protected abstract UniTask StartMovementAsync(CancellationToken token);

    // オブジェクト破棄時にアニメーションや非同期を中断
    protected virtual void OnDestroy()
    {
        cts?.Cancel();
        cts?.Dispose();
    }
}
