using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;

public class LRStopMoveObstacle : ObstacleBase
{
    public float distance = 2f;     // �ړ������i�E�����ɂǂꂾ���������j
    public float duration = 1f;     // �Г��̈ړ��ɂ����鎞�ԁi�b�j
    public float pauseTime = 0.5f;  // ��~���ԁi�b�j�����ǉ��I

    protected override async UniTask StartMovementAsync(CancellationToken token)
    {
        Vector3 start = transform.position;
        Vector3 end = start + Vector3.right * distance;

        while (!token.IsCancellationRequested)
        {
            await MoveOverTime(start, end, duration, token);
            await UniTask.Delay(TimeSpan.FromSeconds(pauseTime), cancellationToken: token); // �����ǉ�

            await MoveOverTime(end, start, duration, token);
            await UniTask.Delay(TimeSpan.FromSeconds(pauseTime), cancellationToken: token); // �����ǉ�
        }
    }

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
