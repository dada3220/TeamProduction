using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject[] stagePrefabs; // �v���n�u�o�^

    private GameObject currentStage;
    private GameObject currentPlayer;
    private int stageIndex = 0;

    private void Start()
    {
        LoadStage(stageIndex);
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

