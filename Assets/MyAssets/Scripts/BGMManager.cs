using UnityEngine;
using UnityEngine.Audio;
using System.Collections;
using System.Collections.Generic;

public class BGMManager : MonoBehaviour
{
    public static BGMManager Instance { get; private set; }

    [Header("BGM設定")]
    public AudioSource audioSource;
    public List<AudioClip> bgmClips;

    [Header("フェード設定")]
    public float fadeDuration = 1.5f;

    private Coroutine currentFade;

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
    }

    public void PlayBGM(int index)
    {
        if (index < 0 || index >= bgmClips.Count)
        {
            Debug.LogWarning("BGM index out of range.");
            return;
        }

        AudioClip clip = bgmClips[index];
        if (audioSource.clip == clip && audioSource.isPlaying) return;

        if (currentFade != null) StopCoroutine(currentFade);
        currentFade = StartCoroutine(FadeToNewClip(clip));
    }

    public void StopBGM()
    {
        if (currentFade != null) StopCoroutine(currentFade);
        currentFade = StartCoroutine(FadeOut());
    }

    private IEnumerator FadeToNewClip(AudioClip newClip)
    {
        yield return StartCoroutine(FadeOut());

        audioSource.clip = newClip;

        // ループ設定を有効
        audioSource.loop = true;

        audioSource.Play();

        yield return StartCoroutine(FadeIn());
    }


    private IEnumerator FadeOut()
    {
        float startVolume = audioSource.volume;

        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            audioSource.volume = Mathf.Lerp(startVolume, 0f, t / fadeDuration);
            yield return null;
        }

        audioSource.volume = 0f;
        audioSource.Stop();
    }

    private IEnumerator FadeIn()
    {
        audioSource.volume = 0f;

        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            audioSource.volume = Mathf.Lerp(0f, 1f, t / fadeDuration);
            yield return null;
        }

        audioSource.volume = 1f;
    }
}
