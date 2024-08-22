using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class sceneUI : MonoBehaviour
{
    [Header("SceneName")] public string SceneName;
    public GameObject SceneButton;
    Button Button1; //외부 노출하지 않은 상태에서 연결하는 작업 시도

    private void Start()
    {
        Button1 = SceneButton.GetComponent<Button>();
        //버튼 컴포넌트를 가져와서 그 기능을 사용합니다.
        //Button1.interactable = false;

        //버튼을 통해 클릭 시 OnClickSceneButton의 작업을 진행하도록 코드를 구현하고 싶습니다.
        Button1.onClick.AddListener(OnClickSceneButton);
        //버튼.onClick.Addlistener(메소드이름);을 통해 버튼에 해당 메소드를 onclick 시 기능으로 추가합니다.
    }
    public void OnClickSceneButton()
    {
        SceneManager.LoadScene(SceneName);
        //반드시 File -> Build Settings을 통해 Scene이 연결이 되어있어야 합니다.
    }
}
