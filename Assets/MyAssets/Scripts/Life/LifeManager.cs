using UnityEngine;
using UnityEngine.Events;

public class LifeManager : MonoBehaviour
{
    [Header("�ݒ�")]
    public int maxLife = 3;  // �ő僉�C�t

    [Header("�C�x���g")]
    public UnityEvent onLifeChanged;  // ���C�t���ω������Ƃ��Ɏ��s����C�x���g
    public UnityEvent onDeath;        // ���C�t��0�ɂȂ����Ƃ��Ɏ��s����C�x���g

    // ���݂̃��C�t
    public int CurrentLife { get; private set; }

    void Start()
    {
        // onDeath �� GameOver ��o�^
        onDeath.AddListener(() =>
        {
            var player = FindFirstObjectByType<PlayerController>();
            if (player != null && GameOverManager.Instance != null)
            {
                GameOverManager.Instance.GameOver(player.gameObject);
            }
        });
    }


    void Awake()
    {
        // �Q�[���J�n���Ƀ��C�t���ő�ɐݒ�
        CurrentLife = maxLife;
    }

    // ���C�t�����炷����
    public void TakeDamage(int amount)
    {
        CurrentLife -= amount;
        SEManager.Instance.PlaySE(0);
        CurrentLife = Mathf.Max(CurrentLife, 0); // ���C�t��0�����ɂȂ�Ȃ��悤�ɐ���
        onLifeChanged?.Invoke(); // ���C�t�ω��C�x���g���Ăяo��

        Debug.Log($"���C�t: {CurrentLife}");

        if (CurrentLife <= 0)
        {
            onDeath?.Invoke(); // ���S�C�x���g���Ăяo��
            Debug.Log("�Q�[���I�[�o�[");


        }
    }

    // ���C�t�����Z�b�g�i�ő僉�C�t�ɖ߂��j
    public void ResetLife()
    {
        CurrentLife = maxLife;
        onLifeChanged?.Invoke();
    }

    // ���C�t��
    public void Heal(int amount)
    {
        int previousLife = CurrentLife;
        CurrentLife = Mathf.Min(CurrentLife + amount, maxLife); // ����𒴂��Ȃ��悤�ɂ���
        if (CurrentLife != previousLife)
        {
            onLifeChanged?.Invoke();
        }
    }

}
