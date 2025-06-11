using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class LifeIconUI : MonoBehaviour
{
    [Header("参照")]
    public LifeManager lifeManager;
    public GameObject heartPrefab;         // ハートアイコンのプレハブ
    public Sprite fullHeartSprite;         // フルのハート
    public Sprite emptyHeartSprite;        // 空のハート
    public Transform heartContainer;       // ハートアイコンを並べる親

    private List<Image> heartImages = new();

    IEnumerator Start()
    {
        if (lifeManager != null)
        {
            yield return null; // 1フレーム待つことで LifeManager.Awake() が完了する

            CreateHearts();
            UpdateHearts();

            lifeManager.onLifeChanged.AddListener(UpdateHearts);
        }
    }

    void OnDestroy()
    {
        if (lifeManager != null)
        {
            lifeManager.onLifeChanged.RemoveListener(UpdateHearts);
        }
    }

    void CreateHearts()
    {
        // ハートアイコンを最大ライフ数分生成
        for (int i = 0; i < lifeManager.maxLife; i++)
        {
            GameObject heart = Instantiate(heartPrefab, heartContainer);
            Image image = heart.GetComponent<Image>();
            heartImages.Add(image);
        }
    }

    void UpdateHearts()
    {
        for (int i = 0; i < heartImages.Count; i++)
        {
            heartImages[i].sprite = i < lifeManager.CurrentLife ? fullHeartSprite : emptyHeartSprite;
        }
    }
}
