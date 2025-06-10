using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Text scoreText; // UI��Text

    public GameObject playerPrefab;
    public GameObject[] stagePrefabs; // �v���n�u�o�^

    private GameObject currentStage;
    private GameObject currentPlayer;
    private int stageIndex = 0;
    private int score = 0;
    private void Awake()
    {
        // �V���O���g���̏�����
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // �V�[�����܂����ł��c���ꍇ
        }
        else
        {
            Destroy(gameObject); // 2�ڈȍ~�͍폜
            return;
        }
    }


    private void Start()
    {
        LoadStage(stageIndex);
        UpdateScoreUI();
    }

    public void AddScore(int amount)
    {
        score += amount;
    }
    private void UpdateScoreUI()
    {
        if(scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }

    public void LoadStage(int index)
    {
        if (currentStage != null) Destroy(currentStage);
        if (currentPlayer != null) Destroy(currentPlayer);

        // �X�e�[�W�v���n�u�𐶐�
        currentStage = Instantiate(stagePrefabs[index]);

        // StartPoint ��T��
        Transform startPoint = currentStage.transform.Find("StartPoint");
        if(startPoint == null)
        {
            Debug.LogError("StartPoint���X�e�[�W�v���n�u�Ɍ�����܂���");
            return;
        }

        currentPlayer = Instantiate(playerPrefab, startPoint.position, Quaternion.identity);
    }

    public void LoadNextStage()
    {
        stageIndex++;
        if(stageIndex < stagePrefabs.Length)
        {
            LoadStage(stageIndex);
        }
        else
        {
            Debug.Log("�S�X�e�[�W�N���A");
        }
    }
}

