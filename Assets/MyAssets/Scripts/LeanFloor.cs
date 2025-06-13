using UnityEngine;

public class LeanFloorn : MonoBehaviour
{
    private Rigidbody2D rb;

    [Header("�����p�x�ɖ߂�ݒ�")]
    public bool returnToInitialRotation = true;
    public float returnSpeed = 10f;

    private float initialRotationZ;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        initialRotationZ = transform.eulerAngles.z;
    }

    private void Update()
    {
        // �v���C���[������Ă��Ȃ����Ɍ��ɖ߂�
        if (returnToInitialRotation && Mathf.Abs(rb.angularVelocity) < 0.1f)
        {
            float currentZ = transform.eulerAngles.z;
            float targetZ = Mathf.MoveTowardsAngle(currentZ, initialRotationZ, returnSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(0f, 0f, targetZ);
        }
    }
}
