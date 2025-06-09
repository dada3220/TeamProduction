using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;

// ��Q���̊��N���X
public abstract class ObstacleBase : MonoBehaviour
{
    protected CancellationTokenSource cts;

    protected virtual void Start()
    {
        cts = new CancellationTokenSource();
        StartMovementAsync(cts.Token).Forget();
    }

    // �h���N���X�œ�����e������
    protected abstract UniTask StartMovementAsync(CancellationToken token);

    // �I�u�W�F�N�g�j�����ɃA�j���[�V������񓯊��𒆒f
    protected virtual void OnDestroy()
    {
        cts?.Cancel();
        cts?.Dispose();
    }
}
