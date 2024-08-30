using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{

    public AudioSource AudioSource;
    // Start is called before the first frame update
    void Awake()
    {
        AudioSource = GetComponent<AudioSource>();
        if(AudioSource)
        {
            AudioSource.playOnAwake = false;
            AudioSource.loop = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (AudioSource && AudioSource.enabled)
            AudioSource.Play();
        else
            Debug.LogError("오디오 소스 컴포넌트가 MISSING 상태입니다.");
    }
 
}
