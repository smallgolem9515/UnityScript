using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class StudyPlayerManager : MonoBehaviour
{
    [Header("-----PlayerMove-----")]
    float playerSpeedWalk = 5f; //�ȴ� �ӵ�
    float playerSpeedRun = 10f; //�޸��� �ӵ�
    public float currentSpeed = 1f; //���� �ӵ�
    float gravity = -9.81f; //�߷� - �⺻���� ����Ƽ������ �߷�
    Vector3 velocity; //���� �ӵ� ����
    CharacterController characterController; //������ٵ�� 3D���� �̷����� ������ �־ ĳ���� ��Ʈ�ѷ��� ����Ѵ�.
    //�� �߷��� ��� ���� �����ؾ��Ѵ�.

    [Header("-----Camera-----")]
    public Transform cameraTransform; //ī�޶� Transform
    public Transform playerHead; //�÷��̾� �Ӹ� ��ġ(1��Ī ����϶� ���)
    public float thirdPersonDistance = 3.0f; // 3��Ī ��忡�� �÷��̾�� ī�޶� �þ� �Ÿ�
    public Vector3 thirdPersonOffset = new Vector3(0, 1.5f, 0); //3��Ī ��忡�� ī�޶������
    float mouseSensitivity = 100f; //���콺 ����
    public Transform playerLookObj; //�÷��̾��� �þ� ��ġ

    [Header("-----Zoom-----")]
    public float zoomedDistance = 1.0f; //ī�޶� Ȯ��� ���� �Ÿ�(3��Ī ����϶� ���)
    public float zoomSpeed = 5f; //Ȯ�� ��Ұ� �Ǵ� �ӵ�
    public float defaultFov = 60f; //�⺻ ī�޶� �þ߰�
    public float zoomedFov = 30f; //Ȯ��� ī�޶� �þ߰� 

    float currentDistance; //���� ī�޶���� �Ÿ�
    float targetDistance; //��ǥ ī�޶� �Ÿ�
    float targetFov; //��ǥ Fov
    bool isZoomed = false; //Ȯ�� ����
    private Coroutine zoomCoroutine; //�ڷ�ƾ�� ����Ͽ� Ȯ��/���
    Camera mainCamera; //ī�޶� ������Ʈ 

    float pitch = 0f; //���Ʒ� ȸ����
    float yaw = 0f; //�¿� ȸ����
    bool isFirstPerson = false; //1��Ī ��� ����
    bool rotaterAroundPlayer = true; //ī�޶� �÷��̾� ������ ȸ���ϴ� ����

    [Header("-----Jump-----")]
    public float jumpHeight = 2f; //���� ����
    bool isGround; //���� �浹 ����
    public LayerMask groundLayer;

    Animator animator;
    float horizontal;
    float vertical;
    public Transform footPosition;
    RaycastHit hit;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        UnityEngine.Cursor.lockState = CursorLockMode.Locked; //Ŀ�� �Ⱥ��̰� ����
        currentDistance = thirdPersonDistance; //�ʱ� ī�޶� �Ÿ��� ����
        targetDistance = thirdPersonDistance; //��ǥ ī�޶� �Ÿ� ����
        targetFov = defaultFov; //�ʱ� Fov ����
        mainCamera = cameraTransform.GetComponent<Camera>();
        mainCamera.fieldOfView = defaultFov; //�⺻ Fov����

        currentSpeed = playerSpeedWalk;
    }
    private void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        animator.SetFloat("Horizontal",horizontal);
        animator.SetFloat("Vertical",vertical);

        if (Input.GetKey(KeyCode.LeftShift)) //�ִϸ��̼��� ���� �޸��� ����
        {
            animator.SetBool("isRun",true);
            currentSpeed = playerSpeedRun;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            animator.SetBool("isRun",false);
            currentSpeed = playerSpeedWalk;
        }
        bool isMoving = characterController.velocity.magnitude > 0.1f;
            Debug.DrawRay(footPosition.position, Vector3.down * 0.2f, Color.red);
        if (horizontal != 0 || vertical != 0)
        {
            StartCoroutine(FootStepSound());
        }
        

        if (Input.GetKeyDown(KeyCode.V)) //VŰ�� 1��Ī/3��Ī ��ȯ
        {
            isFirstPerson = !isFirstPerson;
            Debug.Log(isFirstPerson ? "1��Ī���" : "3��Ī���");
        }
        if(Input.GetKeyDown(KeyCode.LeftAlt) && !isFirstPerson)
        {
            rotaterAroundPlayer = !rotaterAroundPlayer;
            Debug.Log(rotaterAroundPlayer ? "ī�޶� �÷��̾� ������ ȸ��" : "�÷��̾ ���� ȸ��");
        }
        
        updateCameraRotation();
        if (isFirstPerson)
        {
            FirstPersonMovement(); //1��Ī
        }
        else
        {
            ThirdPersonMovement(); //3��Ī
        }
        ZoomMouseRightButton(); //������ ���콺��ư���� ��
        
        Jump(); //����

    }

    IEnumerator FootStepSound()
    {
        
        if (Physics.Raycast(footPosition.position, Vector3.down, out hit, 0.2f, groundLayer)) ;
        {
            if (hit.collider == null)
            {

            }
            else
            {
                
                if (hit.collider.tag == "Metal")
                {
                    StudySoundManager.Instance.PlaySFX("MetalFootStep");
                }
                else if (hit.collider.tag == "Snow")
                {
                    StudySoundManager.Instance.PlaySFX("SnowFootStep");
                }
                else
                {
                    StudySoundManager.Instance.PlaySFX("DefaltFootStep");
                }
            }

        }
        yield return new WaitForSeconds(1.0f);
    }
    void FirstPersonMovement() //1��Ī
    {
        Vector3 move = transform.right * horizontal + transform.forward * vertical;
        characterController.Move(move * currentSpeed * Time.deltaTime);
        mainCamera.transform.position = playerHead.position;
    }
    void ThirdPersonMovement() //3��Ī
    {
        Vector3 move = transform.right * horizontal + transform.forward * vertical + transform.up * velocity.y;
        characterController.Move(move * currentSpeed * Time.deltaTime);
        if(rotaterAroundPlayer)
        {
            UpdateCameraPosition();
        }
        else
        {
            UpdateCameraPositionRotater();    
        }
    }
    void updateCameraRotation()//ī�޶� ȸ��
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime; //���콺���� �������� �ΰ����� �ð��� ���Ѵ�.
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        yaw += mouseX;
        pitch -= mouseY;
        
        pitch = Mathf.Clamp(pitch, -45f, 45f);

        transform.rotation = Quaternion.Euler(0, yaw, 0);
        mainCamera.transform.rotation = Quaternion.Euler(pitch, yaw, 0);
    }
    void UpdateCameraPositionRotater()//ī�޶� ��ġ �����ʿ� ����
    {
        transform.rotation = Quaternion.Euler(0, yaw, 0);
        Vector3 direction = new Vector3(0, 0, -currentDistance);
        //mainCamera.transform.rotation = Quaternion.Euler(pitch, yaw, 0);

        cameraTransform.position = playerLookObj.position + thirdPersonOffset + Quaternion.Euler(pitch, yaw, 0) * direction;
        //ī�޶��� ��ġ�� �ڱ� �ڽſ��� ��¦�� + ������ �ٲ㰡�� + ��¦ �ڿ���

        //ī�޶� �÷��̾��� ��ġ�� ���󰡵��� ����
        cameraTransform.LookAt(playerLookObj.position + new Vector3(0, thirdPersonOffset.y, 0));
    }
    void UpdateCameraPosition()// ī�޶� ��ġ 
    {   
        Vector3 direction = new Vector3(0,0, -currentDistance); //ī�޶� �Ÿ� ����
        Quaternion rotation = Quaternion.Euler(pitch,yaw, 0);

        //ī�޶� �÷��̾��� �����ʿ��� ������ ��ġ�� �̵�
        cameraTransform.position = transform.position + thirdPersonOffset + rotation * direction;
        //ī�޶��� ��ġ�� �ڱ� �ڽſ��� ��¦�� + ������ �ٲ㰡�� + ��¦ �ڿ���

        //ī�޶� �÷��̾��� ��ġ�� ���󰡵��� ����
        cameraTransform.LookAt(transform.position + new Vector3(0, thirdPersonOffset.y, 0));
    }
    void Jump()// ���� �Լ�
    {
        isGround = CheckIsGround();
        if (Input.GetButtonDown("Jump") && isGround)
        {
            StudySoundManager.Instance.PlaySFX("Jump");
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
    }
    bool CheckIsGround()// ����ĳ��Ʈ�� �ٴ� Ȯ��
    {
        RaycastHit hit;
        float rayDistance = 0.2f;

        if(Physics.Raycast(transform.position,Vector3.down,out hit, rayDistance,groundLayer))
        {
            return true;
        }
        return false;
    }
    void ZoomMouseRightButton()// ���콺 ������ ��ư���� ��
    {
        if (Input.GetMouseButtonDown(1)) //������ ���콺 ��ư�� ������ ��
        {
            isZoomed = true;
            if (zoomCoroutine != null)
            {
                StopCoroutine(zoomCoroutine);
            }
            if (isFirstPerson)
            {
                SetTargetFOV(zoomedFov);
                zoomCoroutine = StartCoroutine(ZoomFieldOfView(targetFov));
            }
            else
            {
                SetTargetDistance(zoomedDistance);
                zoomCoroutine = StartCoroutine(ZoomCamera(targetDistance));
            }
        }
        if (Input.GetMouseButtonUp(1))
        {
            isZoomed = false;
            if (zoomCoroutine != null)
            {
                StopCoroutine(zoomCoroutine);
            }
            if (isFirstPerson)
            {
                SetTargetFOV(defaultFov);
                zoomCoroutine = StartCoroutine(ZoomFieldOfView(targetFov));
            }
            else
            {
                SetTargetDistance(thirdPersonDistance);
                zoomCoroutine = StartCoroutine(ZoomCamera(targetDistance));
            }
        }
    }
    public void SetTargetDistance(float distance)
    {
        targetDistance = distance;
    }
    public void SetTargetFOV(float fov)
    {
        targetFov = fov;
    }
    IEnumerator ZoomCamera(float targetDistance)
    {
        while(Mathf.Abs(currentDistance-targetDistance) > 0.01f)
        {
            currentDistance = Mathf.Lerp(currentDistance,targetDistance, Time.deltaTime * zoomSpeed);
            yield return null;
        }
        currentDistance = targetDistance; //��ǥ�Ÿ��� �������� ���� ����
    }
    IEnumerator ZoomFieldOfView(float targetDistance)
    {
        while(Mathf.Abs(mainCamera.fieldOfView - targetFov) > 0.01f)
        {
            mainCamera.fieldOfView = Mathf.Lerp(mainCamera.fieldOfView,targetFov, Time.deltaTime * zoomSpeed);
            yield return null;
        }
        mainCamera.fieldOfView = targetFov;
    }
}
