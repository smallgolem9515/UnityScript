using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Soundsmanager : MonoBehaviour
{
    public static Soundsmanager Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializedAudioClips();//오디오 클립 초기화
        }
        else
        {
            Destroy(gameObject);
        }      
    }
    [Header("SoundSystem")]
    public AudioSource BGMaudioSourse; //배경음악용
    public AudioSource SFXaudioSourse; //효과음용

    private Dictionary<string,AudioClip> BGMClips = new Dictionary<string,AudioClip>();
    private Dictionary<string,AudioClip> SFXClips = new Dictionary<string,AudioClip>();
    

    //Inspector에서 사운드 클립을 추가할 수 있도록 리스트 제공
    [System.Serializable]
    public struct NamedeAudioClip
    {
        public string name;
        public AudioClip clip;
    }
    public NamedeAudioClip[] BGMaudioClip; //배경음악 클립
    public NamedeAudioClip[] SFXaudioClip; //효과음 클립

    private Coroutine currentBGMCoroutine;//현재 실행중인 코루틴을 추적하는 변수

    //Audio Clip 리스트를 Dic로 변환하여 이름으로 접근 가능하게 만드는 함수
    private void InitializedAudioClips()
    {
        foreach (var bgm in BGMaudioClip)
        {
            if(!BGMClips.ContainsKey(bgm.name))
            {
                BGMClips.Add(bgm.name, bgm.clip);
            }
        }
        foreach (var sfx in SFXaudioClip)
        {
            if (!SFXClips.ContainsKey(sfx.name))
            {
                SFXClips.Add(sfx.name, sfx.clip);
            }
        }
    }
    
    public void PlayBGM(string name,float fadeDuration) //배경음 재생함수
    {
        if(BGMClips.ContainsKey(name))
        {
            if(currentBGMCoroutine != null)
            {
                StopCoroutine(currentBGMCoroutine); //기존 페이드 코루틴이 있으면 중단하는 함수
            }

            //현재 BGM이 있는 경우 페이드아웃 후 새로운 BGM으로 페이드인
            currentBGMCoroutine = StartCoroutine(FadeOutBGM(fadeDuration, () =>
            {
                BGMaudioSourse.clip = BGMClips[name];
                BGMaudioSourse.Play();
                currentBGMCoroutine = StartCoroutine(FadeInBGM(fadeDuration));
            }));
            //BGMaudioSourse.clip = BGMClips[name];
            //BGMaudioSourse.Play();
            //BGMaudioSourse.PlayOneShot(BGMaudioSourse.clip);
        }
        else
        {
            Debug.LogWarning("해당 이름의 배경음이 존재하지 않습니다 : " + name);
        }
        
    }
    public void PlaySFX(string name) //효과음 재생함수
    {
        if (SFXClips.ContainsKey(name))
        {
            SFXaudioSourse.PlayOneShot(SFXClips[name]);
        }
        else
        {
            Debug.LogWarning("해당 이름의 효과음이 존재하지 않습니다 : " + name);
        }
    }
    public void StopBGM() //배경음 멈추는 함수
    {
        BGMaudioSourse.Stop();
    }
    public void StopSFX() //효과음 멈추는 함수
    {
        SFXaudioSourse.Stop();
    }
    public void SetBGMVolume(float volume) //배경음 볼륨 조정 함수
    {
        BGMaudioSourse.volume = Mathf.Clamp(volume, 0f, 1f);
    }
    public void SetSFXVolume(float volume) //효과음 볼륨 조정 함수
    {
        SFXaudioSourse.volume = Mathf.Clamp(volume, 0f, 1f);
    }
    //BGM을 페이드아웃 시키는 코루틴
    private IEnumerator FadeOutBGM(float duration, Action onFadeComplete)
    {
        float startVolume = BGMaudioSourse.volume;

        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            BGMaudioSourse.volume = Mathf.Lerp(startVolume,0f,t/duration);
            yield return null;
        }
        BGMaudioSourse.volume = 0f;
        onFadeComplete?.Invoke(); //페이드 아웃이 완료되면 다음 작업 실행
    }
    //BGM 페이드인 시키는 코루틴
    private IEnumerator FadeInBGM(float duration)
    {
        float startVolume = 0f;
        BGMaudioSourse.volume = 0f;

        for(float t = 0;t < duration;t += Time.deltaTime)
        {
            BGMaudioSourse.volume = Mathf.Lerp(startVolume,0.2f,t/duration);
            yield return null;
        }
        BGMaudioSourse.volume= 0.2f;
    }
}
