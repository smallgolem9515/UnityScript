using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    //Player Transform을 받는 변수
    public Transform playerPosition;

    //배경 오브젝트의 Transfrom과 Renderer
    public Transform backGround;
    private Renderer backGroundRenderer;

    //카메라의 절대 이동 범위를 결정하는 변수
    private Vector2 minPosition;
    private Vector2 maxPosition;

    //카메라 시야를 설정하는 변수
    public float cameraFOV = 60f;

    //카메라의 반경
    private float cameraHalfWidth;
    private float cameraHalfHeight;
    // Start is called before the first frame update
    void Start()
    {
        backGroundRenderer = backGround.GetComponent<Renderer>();

        //카메라의 시야에 따른 절반 크기를 계산
        Camera mainCam = Camera.main;
        cameraHalfHeight = mainCam.orthographicSize; //카메라의 세로 반경
        cameraHalfWidth = cameraHalfHeight*mainCam.aspect; //카메라의 종횡비의미, 화면의 너비를 높이로 나눈 값
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
        Vector3 newPosition =  transform.position;

        newPosition.x = playerPosition.position.x;
        newPosition.y = playerPosition.position.y;

        newPosition.x = Mathf.Clamp(newPosition.x,minPosition.x,maxPosition.x);
        newPosition.y = Mathf.Clamp(newPosition.y,minPosition.y,maxPosition.y);

        transform.position = newPosition;

        Camera.main.fieldOfView = cameraFOV;
    }
}
