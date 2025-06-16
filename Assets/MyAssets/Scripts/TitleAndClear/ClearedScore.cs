using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ClearedScore : MonoBehaviour
{
    public TMP_Text scoreText;

    private int nowScore;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        if (scoreText == null)
        {
            GameObject obj = GameObject.Find("Score");

            if (obj != null)
            {
                scoreText = obj.GetComponent<TMP_Text>();
            }
            else
            {
                Debug.Log("スコアオブジェクトが見つかりません");
            }
        }

        nowScore = GameManager.Instance.Score;

        if (scoreText != null)
        {
            scoreText.text = "Score: " + nowScore;
        }


    }

}
