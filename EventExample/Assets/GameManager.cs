using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public void ONDeadPlayer()
    {
        //Invoke("�Լ��̸�",�ð�);
        //�ش� �ð� �Ŀ� �Լ� �̸��� �����մϴ�.
        //��� �ڷ�ƾ���� �����ص� ��� �����ϴ�.
        Invoke("Revival", 5.0f);
    }

    public void Revival()
    {
        Debug.Log("��Ȱ!");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //GetActiveScene()�� ���� �������� ���� ������ ���
    }
}
