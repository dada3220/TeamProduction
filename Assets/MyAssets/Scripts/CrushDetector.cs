using UnityEngine;

public class CrushDetectorByRay : MonoBehaviour
{
    public float checkDistance = 0.1f;           // �㉺�`�F�b�N�̋���
    public LayerMask crushLayer;                 // Stone �� Ground �̃��C���[���w��
    public float requiredCrushTime = 0.05f;       // �ǂꂭ�炢���܂�Ă����瑦�����邩
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
            crushTimer = 0f; // ��������O�ꂽ��^�C�}�[���Z�b�g
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Debug�\���FRaycast��
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.up * checkDistance);
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * checkDistance);
    }
}
