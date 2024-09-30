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
            InitializedAudioClips();//����� Ŭ�� �ʱ�ȭ
        }
        else
        {
            Destroy(gameObject);
        }      
    }
    [Header("SoundSystem")]
    public AudioSource BGMaudioSourse; //������ǿ�
    public AudioSource SFXaudioSourse; //ȿ������

    private Dictionary<string,AudioClip> BGMClips = new Dictionary<string,AudioClip>();
    private Dictionary<string,AudioClip> SFXClips = new Dictionary<string,AudioClip>();
    

    //Inspector���� ���� Ŭ���� �߰��� �� �ֵ��� ����Ʈ ����
    [System.Serializable]
    public struct NamedeAudioClip
    {
        public string name;
        public AudioClip clip;
    }
    public NamedeAudioClip[] BGMaudioClip; //������� Ŭ��
    public NamedeAudioClip[] SFXaudioClip; //ȿ���� Ŭ��

    private Coroutine currentBGMCoroutine;//���� �������� �ڷ�ƾ�� �����ϴ� ����

    //Audio Clip ����Ʈ�� Dic�� ��ȯ�Ͽ� �̸����� ���� �����ϰ� ����� �Լ�
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
    
    public void PlayBGM(string name,float fadeDuration) //����� ����Լ�
    {
        if(BGMClips.ContainsKey(name))
        {
            if(currentBGMCoroutine != null)
            {
                StopCoroutine(currentBGMCoroutine); //���� ���̵� �ڷ�ƾ�� ������ �ߴ��ϴ� �Լ�
            }

            //���� BGM�� �ִ� ��� ���̵�ƿ� �� ���ο� BGM���� ���̵���
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
            Debug.LogWarning("�ش� �̸��� ������� �������� �ʽ��ϴ� : " + name);
        }
        
    }
    public void PlaySFX(string name) //ȿ���� ����Լ�
    {
        if (SFXClips.ContainsKey(name))
        {
            SFXaudioSourse.PlayOneShot(SFXClips[name]);
        }
        else
        {
            Debug.LogWarning("�ش� �̸��� ȿ������ �������� �ʽ��ϴ� : " + name);
        }
    }
    public void StopBGM() //����� ���ߴ� �Լ�
    {
        BGMaudioSourse.Stop();
    }
    public void StopSFX() //ȿ���� ���ߴ� �Լ�
    {
        SFXaudioSourse.Stop();
    }
    public void SetBGMVolume(float volume) //����� ���� ���� �Լ�
    {
        BGMaudioSourse.volume = Mathf.Clamp(volume, 0f, 1f);
    }
    public void SetSFXVolume(float volume) //ȿ���� ���� ���� �Լ�
    {
        SFXaudioSourse.volume = Mathf.Clamp(volume, 0f, 1f);
    }
    //BGM�� ���̵�ƿ� ��Ű�� �ڷ�ƾ
    private IEnumerator FadeOutBGM(float duration, Action onFadeComplete)
    {
        float startVolume = BGMaudioSourse.volume;

        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            BGMaudioSourse.volume = Mathf.Lerp(startVolume,0f,t/duration);
            yield return null;
        }
        BGMaudioSourse.volume = 0f;
        onFadeComplete?.Invoke(); //���̵� �ƿ��� �Ϸ�Ǹ� ���� �۾� ����
    }
    //BGM ���̵��� ��Ű�� �ڷ�ƾ
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
