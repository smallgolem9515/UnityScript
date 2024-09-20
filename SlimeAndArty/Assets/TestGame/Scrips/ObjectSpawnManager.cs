using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawnManager : MonoBehaviour
{
    public GameObject itemPrefab;
    public int poolSize=10; //리스트 오브젝트풀의 사이즈
    public float spawnXPosition=10f; //스폰될 장소
    public float minYPosition=-5f; //최소 Y포인트
    public float maxYPosition=5f; //최대 Y포인트
    public float leftBound=-10f; //왼쪽
    public float spawnInterval=2f;//스폰 딜레이
    public float moveSpeed = 5f; //움직이는 속도
    private float timer = 0f;
    private List<GameObject> objectPool;

    void Start()
    {
        objectPool = new List<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(itemPrefab);
            obj.SetActive(false);
            objectPool.Add(obj);
        }
    }
    void Update()
    {
        timer += Time.time;

        if(timer >= spawnInterval)
        {
            SpawnObjectFromPool();
            timer = 0f;
        }
        ManageActiveObjects();
    }
    void SpawnObjectFromPool() //풀에서 오브젝트 가져오기
    {
        for(int i = 0;i < objectPool.Count;i++)
        {
            if (!objectPool[i].activeInHierarchy)
            {
                float randomY = Random.Range(minYPosition,maxYPosition);
                objectPool[i].transform.position = new Vector3(spawnXPosition, randomY, 0f);
                objectPool[i].SetActive(true);
                break;
            }
        }
    }
    void ManageActiveObjects() //꺼낸 오브젝트 왼쪽끝에 닿으면 비활성화
    {
        foreach(GameObject obj in objectPool)
        {
            if(obj.activeInHierarchy)
            {
                obj.transform.Translate(Vector3.left*moveSpeed*Time.deltaTime);

                if(obj.transform.position.x < leftBound)
                {
                    DeactivateObject(obj);
                }
            }
        }
    }
    public void DeactivateObject(GameObject obj)
    {
        obj.SetActive(false);
    }
}
