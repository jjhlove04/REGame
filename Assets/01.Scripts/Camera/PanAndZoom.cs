using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PanAndZoom : MonoBehaviour
{
    //limit
    [SerializeField]
    private float mapMinX, mapMaxX, mapMinY, mapMaxY;

    //Pan
    [SerializeField]
    private float panSpeed = 2f;

    private CinemachineInputProvider inputProvider;
    private CinemachineVirtualCamera virtualCamera;
    private Transform cameraTransform;

    //Zoom
    private float orthographicSize;
    private float targetorthographicSize;

    const float minOrthographicSize = 40;
    const float maxOrthographicSize = 185f;



    Camera mainCam;
    private void Awake()
    {
        inputProvider = GetComponent<CinemachineInputProvider>();
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        cameraTransform = virtualCamera.VirtualCameraGameObject.transform;
        mainCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        float x = inputProvider.GetAxisValue(0);
        float y = inputProvider.GetAxisValue(1);
        float z = inputProvider.GetAxisValue(2);

        if (x != 0 || y !=0 || z != 0)
        {
            PanScreen(x,y);
        }

        HandleZoom();
    }

    public Vector2 PanDirection(float x, float y)
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
    }

    public void PanScreen(float x, float y)
    {
        Vector2 direction = PanDirection(x, y);
        /*cameraTransform.position = Vector3.Lerp(cameraTransform.position,
            cameraTransform.position+new Vector3(direction.x,0,direction.y) * panSpeed,Time.deltaTime);*/

        cameraTransform.position=ClampCamera(Vector3.Lerp(cameraTransform.position,
            cameraTransform.position + new Vector3(direction.x, 0, direction.y) * panSpeed, Time.deltaTime));
    }
    void HandleZoom()
    {
        float zoomAmount = 2f;
        targetorthographicSize -= Input.mouseScrollDelta.y * zoomAmount;
        targetorthographicSize = Mathf.Clamp(targetorthographicSize, minOrthographicSize, maxOrthographicSize);

        float zoomSpeed = 10f;
        orthographicSize = Mathf.Lerp(orthographicSize, targetorthographicSize, Time.deltaTime * zoomSpeed);

        virtualCamera.transform.position = new Vector3(virtualCamera.transform.position.x, orthographicSize, virtualCamera.transform.position.z);
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

        return new Vector3(newX, 50, newY);
    }
}
