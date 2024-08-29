using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public void ONDeadPlayer()
    {
        //Invoke("함수이름",시간);
        //해당 시간 후에 함수 이름을 실행합니다.
        //대신 코루틴으로 구현해도 상관 없습니다.
        Invoke("Revival", 5.0f);
    }

    public void Revival()
    {
        Debug.Log("부활!");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //GetActiveScene()은 현재 실행중인 씬을 얻어오는 기능
    }
}
