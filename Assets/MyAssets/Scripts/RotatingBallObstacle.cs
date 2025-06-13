using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;

/// <summary>
/// Pivot �𒆐S�ɓS�� (Ball) �� radius �ŉ�]�����A
/// Chain ����� Pivot��Ball �����֌�����
/// </summary>
public class RotatingBallObstacle : ObstacleBase
{
    [Header("�K�{�Q��")]
    public Transform pivot;      // ��]���S
    public Transform ball;       // �S��
    public Transform chain;      // �`�F�[���iSpriteRenderer ��z��j

    [Header("�p�����[�^")]
    public float radius = 2f;        // ��]���a
    public float angularSpeed = 90f; // �x/�b (��: �����v���)

    private float angleDeg;          // ���݊p�x[deg]

    protected override async UniTask StartMovementAsync(CancellationToken token)
    {
        // �����p�x�� 0 ��
        angleDeg = 0f;

        // �`�F�[�����𔼌a�ɍ��킹�ăX�P�[�������iSprite �̕�=1�z��j
        if (chain != null)
        {
            Vector3 s = chain.localScale;
            chain.localScale = new Vector3(radius, s.y, s.z);
        }

        while (!token.IsCancellationRequested)
        {
            // �p�x�X�V
            angleDeg += angularSpeed * Time.deltaTime;
            if (angleDeg >= 360f) angleDeg -= 360f;
            if (angleDeg <= -360f) angleDeg += 360f;

            // �ʒu���v�Z (XY ����)
            float rad = angleDeg * Mathf.Deg2Rad;
            Vector3 offset = new Vector3(Mathf.Cos(rad), Mathf.Sin(rad), 0f) * radius;
            ball.position = pivot.position + offset;

            // �`�F�[���� Pivot��Ball �����֌�����
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
