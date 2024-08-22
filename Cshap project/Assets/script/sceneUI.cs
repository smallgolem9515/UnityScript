using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class sceneUI : MonoBehaviour
{
    [Header("SceneName")] public string SceneName;
    public GameObject SceneButton;
    Button Button1; //�ܺ� �������� ���� ���¿��� �����ϴ� �۾� �õ�

    private void Start()
    {
        Button1 = SceneButton.GetComponent<Button>();
        //��ư ������Ʈ�� �����ͼ� �� ����� ����մϴ�.
        //Button1.interactable = false;

        //��ư�� ���� Ŭ�� �� OnClickSceneButton�� �۾��� �����ϵ��� �ڵ带 �����ϰ� �ͽ��ϴ�.
        Button1.onClick.AddListener(OnClickSceneButton);
        //��ư.onClick.Addlistener(�޼ҵ��̸�);�� ���� ��ư�� �ش� �޼ҵ带 onclick �� ������� �߰��մϴ�.
    }
    public void OnClickSceneButton()
    {
        SceneManager.LoadScene(SceneName);
        //�ݵ�� File -> Build Settings�� ���� Scene�� ������ �Ǿ��־�� �մϴ�.
    }
}
