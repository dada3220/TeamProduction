using UnityEngine;
using Cysharp.Threading.Tasks;      
using System.Threading;             // CancellationToken�p

// ��Q���̈ړ��𐧌䂷��N���X�i���E�ɉ�������^�C�v�j
public class LRMoveObstacle : ObstacleBase
{
    public float distance = 2f;     // �ړ������i�E�����ɂǂꂾ���������j
    public float duration = 1f;     // �Г��̈ړ��ɂ����鎞�ԁi�b�j

    // ObstacleBase �Œ�`���ꂽ���ۃ��\�b�h������
    protected override async UniTask StartMovementAsync(CancellationToken token)
    {
        // �����ʒu���擾
        Vector3 start = transform.position;
        // �ړ���̈ʒu
        Vector3 end = start + Vector3.right * distance;

        // �L�����Z�������܂ō��E�ɉ���
        while (!token.IsCancellationRequested)
        {
            // �E�ֈړ�
            await MoveOverTime(start, end, duration, token);
            // ���ֈړ�
            await MoveOverTime(end, start, duration, token);
        }
    }

    // �w�肳�ꂽ���Ԃňړ�����񓯊����\�b�h
    private async UniTask MoveOverTime
        (Vector3 from, Vector3 to, float time, CancellationToken token)
    {
        float elapsed = 0f;

        // �o�ߎ��Ԃ��ړI�̈ړ����ԂɒB����܂ŌJ��Ԃ�
        while (elapsed < time)
        {
            // �����I�u�W�F�N�g���폜���ꂽ�ȂǂŃL�����Z�����ꂽ�璆�f
            if (token.IsCancellationRequested) return;

            // ���Ԃ����Z
            elapsed += Time.deltaTime;

            // t = 0.0�`1.0 �͈̔͂ɕ␳
            float t = Mathf.Clamp01(elapsed / time);

            // ���`��ԁiLerp�j�ňʒu���v�Z
            transform.position = Vector3.Lerp(from, to, t);

            // 1�t���[���ҋ@�i�L�����Z���Ή��j
            await UniTask.Yield(PlayerLoopTiming.Update, token);
        }

        transform.position = to;
    }
}
