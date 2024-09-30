using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    Camera mainCam;
    Vector3 defaltPosi;
    int xSize = 13;
    int ySize = 6;

    void Start()
    {
        mainCam = Camera.main;
        defaltPosi = transform.GetChild(0).localPosition;
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        if(CameraManager.instance.isShake == false)
        {
            if (PlayerManagerSlime.instance.transform.position.x > defaltPosi.x + xSize)
            {
                PlayerMoveMap(true, xSize);
            }
            else if (PlayerManagerSlime.instance.transform.position.x < defaltPosi.x - xSize)
            {
                PlayerMoveMap(true, -xSize);
            }
            else if (PlayerManagerSlime.instance.transform.position.y > defaltPosi.y + ySize)
            {
                PlayerMoveMap(false, ySize);
            }
            else if (PlayerManagerSlime.instance.transform.position.y < defaltPosi.y - ySize)
            {
                PlayerMoveMap(false, -ySize);
            }
        }      
    }
    void PlayerMoveMap(bool XY,int plusMinus)
    {
        if (XY)
        {
            mainCam.transform.position = new Vector3(defaltPosi.x + (plusMinus * 2), defaltPosi.y, -10);
            defaltPosi = mainCam.transform.position;
        }
        else if (!XY)
        {
            mainCam.transform.position = new Vector3(defaltPosi.x, defaltPosi.y + (plusMinus * 2), -10);
            defaltPosi = mainCam.transform.position;
        }
        PlayerManagerSlime.instance.count = PlayerManagerSlime.instance.maxCount;
        PlayerManagerSlime.instance.isClear = true;
        Invoke("Clear", 0.1f);
    }
    void Clear()
    {
        PlayerManagerSlime.instance.isClear = false;
    }
    

}
