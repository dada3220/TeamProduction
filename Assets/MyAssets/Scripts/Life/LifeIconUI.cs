using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class LifeIconUI : MonoBehaviour
{
    [Header("�Q��")]
    public LifeManager lifeManager;
    public GameObject heartPrefab;         // �n�[�g�A�C�R���̃v���n�u
    public Sprite fullHeartSprite;         // �t���̃n�[�g
    public Sprite emptyHeartSprite;        // ��̃n�[�g
    public Transform heartContainer;       // �n�[�g�A�C�R������ׂ�e

    private List<Image> heartImages = new();

    IEnumerator Start()
    {
        if (lifeManager != null)
        {
            yield return null; // 1�t���[���҂��Ƃ� LifeManager.Awake() ����������

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
        // �n�[�g�A�C�R�����ő僉�C�t��������
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
