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
    [Header("Zone1")]
    public GameObject monsterZone1;
    public bool isMonsterZone1 = false;
    public GameObject[] zone1Monsters;
    [Header("Zone2")]
    public GameObject Zone2;
    [Header("Zone4")]
    public GameObject monsterZone4;
    public GameObject trapDoor;
    public bool isMonsterZone4 = false;
    public GameObject[] zone4Monsters;
    Camera mainCam;
    Vector3 defaltPosi;
    int xSize = 11;
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
                SoundManager.instance.PlaySFX("Clear");
                SoundManager.instance.PlayBGM("Field",1f);

            }
        }
        if (isMonsterZone4)
        {
            if (zone4Monsters[0].tag == "Jelly")
            {
                monsterZone4.SetActive(false);
                isMonsterZone4 = false;
                trapDoor.SetActive(true);
                SoundManager.instance.PlayBGM("Field",1f);
                SoundManager.instance.PlaySFX("TrapDoor");
            }
        }
    }
    public void MonsterZone(int id)
    {
        SoundManager.instance.PlayBGM("Battle", 1f);
        GameObject[] zoneMonsters = null;
        if (id == 1)
        {
            monsterZone1.SetActive(true);
            isMonsterZone1 = true;
            zoneMonsters = zone1Monsters;
        }
        else if (id == 4)
        {
            monsterZone4.SetActive(true);
            isMonsterZone4 = true;
            zoneMonsters = zone4Monsters;
        }
        for (int i = 0; i < zone1Monsters.Length; i++)
        {
            zoneMonsters[i].gameObject.SetActive(true);
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
