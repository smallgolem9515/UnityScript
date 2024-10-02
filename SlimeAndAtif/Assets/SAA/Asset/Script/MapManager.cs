using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public static MapManager Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public GameObject monsterZone1;
    public bool isMonsterZone1 = false;
    public GameObject[] zone1Monsters;
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
        if(isMonsterZone1)
        {
            if (zone1Monsters[0].tag == "Jelly" &&
                zone1Monsters[1].tag == "Jelly" &&
                zone1Monsters[2].tag == "Jelly")
            {
                monsterZone1.SetActive(false);
                isMonsterZone1 = false;
            }
        }
    }
    public void MonsterZone1()
    {
        for (int i = 0; i < zone1Monsters.Length; i++)
        {
            zone1Monsters[i].gameObject.SetActive(true);
        }
        monsterZone1.SetActive(true);
        isMonsterZone1 = true;
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
