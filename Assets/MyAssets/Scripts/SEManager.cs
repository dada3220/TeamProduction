using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SEManager : SingletonMonoBehaviour<SEManager>
{
    

    [Header("SE設定")]
    public AudioSource audioSource;
    public List<AudioClip> seClips;
    public AudioMixer audioMixer;

    private Dictionary<string, AudioClip> seDict;

    protected override void  Awake()
    {
        
        CheckInstance();
        // 名前で再生したい場合の辞書化
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
    /// インデックスでSEを再生
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
    /// 名前でSEを再生（AudioClip.nameがキー）
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
