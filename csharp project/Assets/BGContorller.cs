using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGContorller : MonoBehaviour
{
    public float speed = 0.02f; 
    void Update()
    {
        float offsetx = speed * Time.deltaTime;
        transform.GetComponent<Renderer>().material.mainTextureOffset += new Vector2(offsetx, 0);
        //�������� ���� ��Ƽ���� �����ϰ�
        //��Ƽ������ �ؽ��� �������� �����ϴ� �ڵ�
    }
}
