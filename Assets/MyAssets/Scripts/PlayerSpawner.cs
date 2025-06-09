using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [Header("�v���C���[�ƃX�^�[�g�n�_�̎Q��")]
    public GameObject playerPrefab;
    public Transform startPoint;
    void Start()
    {

        if (playerPrefab != null && startPoint != null)
        {
            Instantiate(playerPrefab, startPoint.position, Quaternion.identity);
        }
        else
        {
            Debug.LogError("StartPoint �܂��� playerPrefab ��������܂���I");
        }
    }
}
