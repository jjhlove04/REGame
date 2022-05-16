using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.EventSystems;

public class PanAndZoom : MonoBehaviour
{
    //limit
    [SerializeField]
    private float mapMinX, mapMaxX, mapMinY, mapMaxY;

    //Pan
    [SerializeField]
    private float panSpeed = 2f;

    private float speed;

    private CinemachineInputProvider inputProvider;
    private CinemachineVirtualCamera virtualCamera;
    private Transform cameraTransform;

    //Zoom
    private float orthographicSize;
    public float targetorthographicSize;

    const float minOrthographicSize = 40;
    const float maxOrthographicSize = 185f;

    private bool cameraStop = false;




    Camera mainCam;
    private void Awake()
    {
        inputProvider = GetComponent<CinemachineInputProvider>();
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        cameraTransform = virtualCamera.VirtualCameraGameObject.transform;
        mainCam = Camera.main;

        speed = panSpeed;

        targetorthographicSize = 70;
    }


    Vector3 CamMove()
    {
        Vector3 pos = Vector3.zero;

        pos.x -= Input.GetAxis("Mouse X");
        pos.z = Input.GetAxis("Mouse Y") * -1;

        //centralAxis.rotation = Quaternion.Euler(new Vector3(centralAxis.rotation.x + mouseY, centralAxis.rotation.y + mouseX, 0) * camSpeed);

        return pos;
    }
    // Start is called before the first frame update

    // Update is called once per frame

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.Y))
        {
            cameraStop = !cameraStop;
        }*/

        if (!cameraStop)
        {
            /*float x = inputProvider.GetAxisValue(0);
            float y = inputProvider.GetAxisValue(1);
            float z = inputProvider.GetAxisValue(2);*/

            float x = 0;
            float y = 0;
            float z = 0;

            x = Input.GetAxisRaw("Horizontal");

            y = Input.GetAxisRaw("Vertical");

            if (Input.GetMouseButton(1))
            {
                Vector3 pos = Vector3.zero;

                pos = CamMove();

                x = pos.x;
                y = pos.z;
            }


            if (x != 0 || y != 0 || z != 0 && !cameraStop)
            {
                PanScreen(x, y);
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed = panSpeed * 2;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = panSpeed;
        }

        HandleZoom();
    }

    /*public Vector2 PanDirection(float x, float y)
    {
        Vector2 direction = Vector2.zero;

        if(y >= Screen.height * 0.95f)
        {
            direction.y += 1;
        }
        else if (y <= Screen.height * 0.05f)
        {
            direction.y -= 1;
        }
        if (x >= Screen.width * 0.95f)
        {
            direction.x += 1;
        }
        else if(x <= Screen.width * 0.05f)
        {
            direction.x -= 1;
        }
        return direction;
    }*/

    public Vector2 PanDirection(float x, float y)
    {
        Vector2 direction = Vector2.zero;

        direction.x = x;
        direction.y = y;

        return direction;
    }

    public void PanScreen(float x, float y)
    {
        Vector2 direction = PanDirection(x, y);
        /*cameraTransform.position = Vector3.Lerp(cameraTransform.position,
            cameraTransform.position+new Vector3(direction.x,0,direction.y) * panSpeed,Time.deltaTime);*/

        cameraTransform.position = ClampCamera(Vector3.Lerp(cameraTransform.position,
            cameraTransform.position + new Vector3(direction.x, 0, direction.y) * speed, Time.unscaledDeltaTime));
    }

    void HandleZoom()
    {   
        float zoomAmount = 2f;
        targetorthographicSize -= Input.mouseScrollDelta.y * zoomAmount;
        targetorthographicSize = Mathf.Clamp(targetorthographicSize, minOrthographicSize, maxOrthographicSize);

        float zoomSpeed = 10f;
        orthographicSize = Mathf.Lerp(orthographicSize, targetorthographicSize, Time.unscaledDeltaTime * zoomSpeed);

        cameraTransform.position = Vector3.Lerp(cameraTransform.position,ClampCamera(new Vector3(cameraTransform.position.x, orthographicSize, cameraTransform.position.z)), Time.deltaTime * 5);
        virtualCamera.m_Lens.FarClipPlane = virtualCamera.transform.position.y+5f;
        virtualCamera.m_Lens.FieldOfView = 50;
    }

    private Vector3 ClampCamera(Vector3 targetPosition)
    {
        float frustumHeight = (2.0f * virtualCamera.transform.position.y * Mathf.Tan(mainCam.fieldOfView * 0.5f * Mathf.Deg2Rad))*0.5f;
        float frustumWidth = (2*frustumHeight * mainCam.aspect)*0.5f;

        float minX = mapMinX + frustumWidth;
        float maxX = mapMaxX - frustumWidth;
        float minY = mapMinY + frustumHeight;
        float maxY = mapMaxY - frustumHeight;

        float newX = Mathf.Clamp(targetPosition.x, minX, maxX);
        float newY = Mathf.Clamp(targetPosition.z, minY, maxY);

        return new Vector3(newX, orthographicSize, newY);
    }

    /*public void OnPointerEnter(PointerEventData eventData)
    {
        cameraStop = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        cameraStop = false;
    }*/
}
