using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    private void Update()
    {
        if(Soundsmanager.Instance.BGMaudioSourse.clip == null)
        {
            string sceneName =  SceneManager.GetActiveScene().name;
            Soundsmanager.Instance.OnSceneLoaded(sceneName);
        }

    }
}
