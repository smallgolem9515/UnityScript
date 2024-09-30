using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private Vector3 originalPos; //ī�޶� ���� ��ġ
    public bool isShake = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public IEnumerator Shake(float duration, float magnitude)
    {
        if (Camera.main != null)
        {
            originalPos = Camera.main.transform.position;
        }
        else
        {
            Debug.Log("���� ī�޶� ã�� �� �����ϴ�. MainCamera Tag�� Ȯ�����ּ���.");
            yield break;
        }
        isShake = true;
        float elapsed = 0.0f;
        while (elapsed < duration)
        {
            float x = Random.Range(-1, 1f) * magnitude;
            float y = Random.Range(-1, 1f) * magnitude;

            transform.position = new Vector3(originalPos.x + x, originalPos.y + y, -10);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.position = originalPos;
        isShake = false;
    }
}
