using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            InitializedAudioClips();
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public AudioSource BGMaudioSource;
    public AudioSource SFXaudioSource;

    private Dictionary<string,AudioClip> BGMaudioDic = new Dictionary<string,AudioClip>();
    private Dictionary<string,AudioClip> SFXaudioDic = new Dictionary<string,AudioClip>();
    [System.Serializable]
    public struct NamedeAudioClip
    {
        public string name;
        public AudioClip clip;
    }

    public NamedeAudioClip[] BGMaudioClips;
    public NamedeAudioClip[] SFXaudioClips;

    private Coroutine currentBGMCoroutine;
    private void InitializedAudioClips()
    {
        foreach (var bgm in BGMaudioClips)
        {
            if(!BGMaudioDic.ContainsKey(bgm.name))
            {
                BGMaudioDic.Add(bgm.name, bgm.clip);
            }
        }
        foreach(var sfx in SFXaudioClips)
        {
            if(!SFXaudioDic.ContainsKey(sfx.name))
            {
                SFXaudioDic.Add(sfx.name, sfx.clip);
            }
        }
    }

    public void PlayBGM(string name,float duration)
    {
        if(BGMaudioDic.ContainsKey(name))
        {
            if (currentBGMCoroutine != null)
            {
                StopCoroutine(currentBGMCoroutine);
            }
            currentBGMCoroutine = StartCoroutine(FadeOutBGM(duration, () =>
            {
                BGMaudioSource.clip = BGMaudioDic[name];
                BGMaudioSource.Play();
                currentBGMCoroutine = StartCoroutine(FadeInBGM(duration));
            }));
        }
        else
        {
            Debug.LogWarning("해당 이름의 배경음이 존재하지 않습니다 : " + name);
        }
    }
    public void PlaySFX(string name)
    {
        if(SFXaudioDic.ContainsKey(name))
        {
            SFXaudioSource.PlayOneShot(SFXaudioDic[name]);
        }
        else
        {
            Debug.LogWarning("해당 이름의 효과음이 존재하지 않습니다 : " + name);
        }
    }
    public void StopBGM()
    {
        BGMaudioSource.Stop();
    }
    public void StopSFX()
    {
        SFXaudioSource.Stop();
    }
    public void SetBGMVolume(float volume)
    {
        BGMaudioSource.volume = Mathf.Clamp(volume, 0f, 1f);
    }
    public void SetSFXVolume(float volume)
    {
        SFXaudioSource.volume = Mathf.Clamp(volume, 0f, 1f);
    }
    private IEnumerator FadeOutBGM(float duration,Action onFadeComplete)
    {
        float startVolume = BGMaudioSource.volume;

        for(float t = 0; t < duration; t += Time.deltaTime)
        {
            BGMaudioSource.volume = Mathf.Lerp(startVolume,0f,t/duration);
            yield return null;
        }
        BGMaudioSource.volume=0f;
        onFadeComplete?.Invoke();
    }
    private IEnumerator FadeInBGM(float duration)
    {
        float startVolume = 0f;
        BGMaudioSource.volume = 0f;

        for(float t = 0;t < duration;t += Time.deltaTime)
        {
            BGMaudioSource.volume = Mathf.Lerp(startVolume, 1f, t / duration);
            yield return null;
        }
        BGMaudioSource.volume=1f;
    }
}
