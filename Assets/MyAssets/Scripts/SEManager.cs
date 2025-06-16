using UnityEngine;
using System.Collections.Generic;

public class SEManager : MonoBehaviour
{
    public static SEManager Instance { get; private set; }

    [Header("SE設定")]
    public AudioSource audioSource;
    public List<AudioClip> seClips;

    private Dictionary<string, AudioClip> seDict;

    void Awake()
    {
        // シングルトン化
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

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
