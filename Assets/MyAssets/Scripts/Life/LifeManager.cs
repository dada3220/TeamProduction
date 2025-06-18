using UnityEngine;
using UnityEngine.Events;

public class LifeManager : MonoBehaviour
{
    [Header("設定")]
    public int maxLife = 3;  // 最大ライフ

    [Header("イベント")]
    public UnityEvent onLifeChanged;  // ライフが変化したときに実行するイベント
    public UnityEvent onDeath;        // ライフが0になったときに実行するイベント

    // 現在のライフ
    public int CurrentLife { get; private set; }

    void Start()
    {
        // onDeath に GameOver を登録
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
        // ゲーム開始時にライフを最大に設定
        CurrentLife = maxLife;
    }

    // ライフを減らす処理
    public void TakeDamage(int amount)
    {
        CurrentLife -= amount;
        SEManager.Instance.PlaySE(0);
        CurrentLife = Mathf.Max(CurrentLife, 0); // ライフが0未満にならないように制限
        onLifeChanged?.Invoke(); // ライフ変化イベントを呼び出す

        Debug.Log($"ライフ: {CurrentLife}");

        if (CurrentLife <= 0)
        {
            onDeath?.Invoke(); // 死亡イベントを呼び出す
            Debug.Log("ゲームオーバー");


        }
    }

    // ライフをリセット（最大ライフに戻す）
    public void ResetLife()
    {
        CurrentLife = maxLife;
        onLifeChanged?.Invoke();
    }

    // ライフ回復
    public void Heal(int amount)
    {
        int previousLife = CurrentLife;
        CurrentLife = Mathf.Min(CurrentLife + amount, maxLife); // 上限を超えないようにする
        if (CurrentLife != previousLife)
        {
            onLifeChanged?.Invoke();
        }
    }

}
