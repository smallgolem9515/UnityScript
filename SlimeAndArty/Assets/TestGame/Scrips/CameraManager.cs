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
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    //Player Transform�� �޴� ����
    public Transform playerPosition;

    //��� ������Ʈ�� Transfrom�� Renderer
    public Transform backGround;
    private Renderer backGroundRenderer;

    //ī�޶��� ���� �̵� ������ �����ϴ� ����
    private Vector2 minPosition;
    private Vector2 maxPosition;

    //ī�޶� �þ߸� �����ϴ� ����
    public float cameraFOV = 60f;

    //ī�޶��� �ݰ�
    private float cameraHalfWidth;
    private float cameraHalfHeight;

    public bool isShake = false;
    public Vector3 shakePosition;
    // Start is called before the first frame update
    void Start()
    {
        backGroundRenderer = backGround.GetComponent<Renderer>();
        //ī�޶��� �þ߿� ���� ���� ũ�⸦ ���
        Camera mainCam = Camera.main;
        cameraHalfHeight = mainCam.orthographicSize; //ī�޶��� ���� �ݰ�
        cameraHalfWidth = cameraHalfHeight*mainCam.aspect; //ī�޶��� ��Ⱦ���ǹ�, ȭ���� �ʺ� ���̷� ���� ��
        CalculateCameraBounds();
    }
    void CalculateCameraBounds()
    {
        Bounds backGroundBounds = backGroundRenderer.bounds;

        minPosition = new Vector2(backGroundBounds.min.x + cameraHalfWidth, backGroundBounds.min.y + cameraHalfHeight);
        maxPosition = new Vector2(backGroundBounds.max.x - cameraHalfWidth, backGroundBounds.max.y - cameraHalfHeight);

    }

    private void LateUpdate()
    {
        if(!isShake)
        {
            Vector3 newPosition = transform.position;

            newPosition.x = playerPosition.position.x;
            newPosition.y = playerPosition.position.y;

            newPosition.x = Mathf.Clamp(newPosition.x, minPosition.x, maxPosition.x);
            newPosition.y = Mathf.Clamp(newPosition.y, minPosition.y, maxPosition.y);

            transform.position = newPosition;
            shakePosition = newPosition;
            Camera.main.fieldOfView = cameraFOV;
        }
    }
}
