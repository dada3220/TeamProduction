using UnityEngine;


public class GoalPoint : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("�S�[���I");

            // ��F���̃V�[����ǂݍ��ށi���O�܂��͔ԍ��j
            // SceneManager.LoadScene("NextStage");
            // �܂��́F
            gameManager.LoadNextStage();

            // ���̓S�[������Debug���O���o������
        }
    }
}
