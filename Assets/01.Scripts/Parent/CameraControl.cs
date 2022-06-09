using UnityEngine;
using Cinemachine;

public class CameraControl : MonoBehaviour
{

    [SerializeField]
    private CinemachineVirtualCamera cinemachineVirtualCamera;
    private CinemachineFramingTransposer offset;

    private float orthographicSize;
    private float targetorthographicSize;

    const float minOrthographicSize = 35;
    const float maxOrthographicSize = 60;

    private Camera mainCam;

    [SerializeField]
    private LayerMask ground;

    // Start is called before the first frame update
    void Start()
    {
        mainCam = Camera.main;
        offset = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
    }

    void Update()
    {
        GroundDistance();
        HandleZoom();
    }


    void HandleZoom()
    {
        float zoomAmount = 2f;
        targetorthographicSize -= Input.mouseScrollDelta.y * zoomAmount;
        targetorthographicSize = Mathf.Clamp(targetorthographicSize, minOrthographicSize, maxOrthographicSize);

        float zoomSpeed = 10f;
        orthographicSize = Mathf.Lerp(orthographicSize, targetorthographicSize, Time.deltaTime * zoomSpeed);

        offset.m_TrackedObjectOffset.y = orthographicSize;
    }

    void MouseMove(float distance)
    {
        Vector3 mousePos = mainCam.ScreenToWorldPoint( new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance));
        transform.position = mousePos;
    }

    void GroundDistance()
    {
        RaycastHit hit;
        float groundDistance = 0;
        if(Physics.Raycast(mainCam.transform.position, Vector3.down, out hit, ground))
        {
            groundDistance = mainCam.transform.position.y - hit.transform.position.y;
            MouseMove(groundDistance);
        }

    }
}
