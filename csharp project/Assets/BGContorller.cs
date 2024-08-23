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
        //렌더러를 통해 머티리얼에 접근하고
        //머티리얼의 텍스쳐 오프셋을 설정하는 코드
    }
}
