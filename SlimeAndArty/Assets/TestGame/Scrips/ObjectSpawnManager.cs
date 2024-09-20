using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawnManager : MonoBehaviour
{
    public GameObject itemPrefab;
    public int poolSize=10; //����Ʈ ������ƮǮ�� ������
    public float spawnXPosition=10f; //������ ���
    public float minYPosition=-5f; //�ּ� Y����Ʈ
    public float maxYPosition=5f; //�ִ� Y����Ʈ
    public float leftBound=-10f; //����
    public float spawnInterval=2f;//���� ������
    public float moveSpeed = 5f; //�����̴� �ӵ�
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
    void SpawnObjectFromPool() //Ǯ���� ������Ʈ ��������
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
    void ManageActiveObjects() //���� ������Ʈ ���ʳ��� ������ ��Ȱ��ȭ
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
