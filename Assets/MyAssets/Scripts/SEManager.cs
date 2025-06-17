using UnityEngine;
using System.Collections.Generic;

public class SEManager : SingletonMonoBehaviour<SEManager>
{
    

    [Header("SE�ݒ�")]
    public AudioSource audioSource;
    public List<AudioClip> seClips;

    private Dictionary<string, AudioClip> seDict;

    protected override void  Awake()
    {
        
        CheckInstance();
        // ���O�ōĐ��������ꍇ�̎�����
        seDict = new Dictionary<string, AudioClip>();
        foreach (var clip in seClips)
        {
            if (clip != null && !seDict.ContainsKey(clip.name))
            {
                seDict.Add(clip.name, clip);
            }
        }
    }

    /// <summary>
    /// �C���f�b�N�X��SE���Đ�
    /// </summary>
    public void PlaySE(int index)
    {
        if (index < 0 || index >= seClips.Count)
        {
            Debug.LogWarning("SE index out of range.");
            return;
        }

        audioSource.PlayOneShot(seClips[index]);
    }

    /// <summary>
    /// ���O��SE���Đ��iAudioClip.name���L�[�j
    /// </summary>
    public void PlaySE(string name)
    {
        if (seDict.TryGetValue(name, out var clip))
        {
            audioSource.PlayOneShot(clip);
        }
        else
        {
            Debug.LogWarning($"SE with name '{name}' not found.");
        }
    }
}
