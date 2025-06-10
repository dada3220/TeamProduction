using UnityEngine;


public class GoalPoint : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("�S�[���I");

           
            GameManager.Instance.LoadNextStage();

        }
    }
}
