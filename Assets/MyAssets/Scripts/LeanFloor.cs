using UnityEngine;

public class LeanFloorn : MonoBehaviour
{
    private Rigidbody2D rb;

    [Header("初期角度に戻る設定")]
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
        // プレイヤーが乗っていない時に元に戻す
        if (returnToInitialRotation && Mathf.Abs(rb.angularVelocity) < 0.1f)
        {
            float currentZ = transform.eulerAngles.z;
            float targetZ = Mathf.MoveTowardsAngle(currentZ, initialRotationZ, returnSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(0f, 0f, targetZ);
        }
    }
}
