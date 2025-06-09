using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;

// �㉺�ɉ��������Q���N���X
public class UDMoveObstacle : ObstacleBase
{
    public float distance = 2f;   // ������Ɉړ����鋗��
    public float duration = 1f;   // �Г��ړ��̎��ԁi�b�j

    // ��Q���̈ړ����W�b�N�i�㉺�����j
    protected override async UniTask StartMovementAsync(CancellationToken token)
    {
        // ���݂̈ʒu���擾�i�J�n�ʒu�j
        Vector3 start = transform.position;
        // ������ւ̈ړ�����v�Z�i�オ +Y�j
        Vector3 end = start + Vector3.up * distance;

        // �L�����Z�������܂ŏ㉺�ɉ�����������
        while (!token.IsCancellationRequested)
        {
            // ��ֈړ�
            await MoveOverTime(start, end, duration, token);
            // ���֖߂�
            await MoveOverTime(end, start, duration, token);
        }
    }

    // �w�莞�Ԃ� from �� to �Ɉړ������ԏ���
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
