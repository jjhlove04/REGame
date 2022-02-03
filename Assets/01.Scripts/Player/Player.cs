using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Player : MonoBehaviour
{
    private TrainManager createTrain;
    private Rigidbody rigid;
    private BoxCollider areaSize;
    private PlayerAnimation playerAnimation;


    [SerializeField]
    private float rimitMinX, rimitMaxX;

    [SerializeField]
    private float rimitMinZ, rimitMaxZ;

    [Header("카메라 관련 변수")]
    [SerializeField]
    private CinemachineVirtualCamera cinemachineVirtualCamera;

    private float orthographicSize;
    private float targetOrthographicSize;

    const float minOrthographicSize = 10f;
    const float maxOrthographicSize = 30f;


    [Header("플레이어 달리는 속도 (걷는 속도 * runspeed)")]
    [SerializeField]
    private float runspeed = 2;
    [Header("플레이어 걷는 속도")]
    [SerializeField]
    private float Initspeed = 5;
    [SerializeField]
    private float rotateSpeed = 6f;

    private float speed;

    float horizontalDir;
    float verticalDir;
    [Header("플레이어 상호작용 거리")]
    float playerVision = 5;

    private void Start()
    {
        //orthographicSize = cinemachineVirtualCamera.m_Lens.FieldOfView;
        rigid = GetComponent<Rigidbody>();
        createTrain = FindObjectOfType<TrainManager>();
        playerAnimation = GetComponent<PlayerAnimation>();
        speed = Initspeed;
    }

    private void Update()
    {
        Move();
        Rotate();
        RimitPosition();
        HandleZoom();
    }

    void HandleZoom()
    {
        float zoomAmount = 2f;
        targetOrthographicSize += -Input.mouseScrollDelta.y * zoomAmount;
        targetOrthographicSize = Mathf.Clamp(targetOrthographicSize, minOrthographicSize, maxOrthographicSize);

        // 부드러운 느낌을 주기 위한 코드
        float zoomSpeed = 5f;
        //orthographicSize = Mathf.Lerp(orthographicSize, targetOrthographicSize, Time.deltaTime * zoomSpeed);

        //cinemachineVirtualCamera.m_Lens.OrthographicSize = orthographicSize;
    }

    /// <summary>
    /// 플레이어 이동 함수
    ///</summary>
    void Move()
    {

        if (Input.GetAxis("Horizontal") != 0 && !playerAnimation.upperanimator.GetBool("Attack"))
        {
            horizontalDir = Input.GetAxis("Horizontal");

            MoveHorizontal(horizontalDir);
            playerAnimation.SetSide(true);
            playerAnimation.IsRight(horizontalDir);
        }

        if (Input.GetAxis("Vertical") != 0 && !playerAnimation.upperanimator.GetBool("Attack"))
        {
            verticalDir = Input.GetAxis("Vertical");

            MoveVertical(verticalDir);
            playerAnimation.SetDiretion(verticalDir);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed = Initspeed * runspeed;
            playerAnimation.SetRun(true);
        }

        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = Initspeed;
            playerAnimation.SetRun(false);
        }



        if (Input.GetMouseButtonDown(0))
        {
            playerAnimation.SetAttack(true);
            StartCoroutine(IsAttacking());
        }


        playerAnimation.SetSide(!(Input.GetAxis("Horizontal") == 0));
        playerAnimation.SetMove(!(Input.GetAxis("Vertical") == 0 && Input.GetAxis("Horizontal") == 0));
    }
    /// <summary>
    /// 플레이어 회전 함수
    ///</summary>
    private void Rotate()
    {
        if (!playerAnimation.upperanimator.GetBool("Attack"))
        {
            Vector3 target = PlayerInput.Instance.mousePos;
            target.y = 0;
            Vector3 v = target - transform.position;
            float degree = Mathf.Atan2(v.x, v.z) * Mathf.Rad2Deg;
            float rot = Mathf.LerpAngle(transform.eulerAngles.y, degree, Time.deltaTime * rotateSpeed);
            transform.eulerAngles = new Vector3(0, rot, 0);
        }
    }

    /// <summary>
    /// 플레이어 세로 이동 함수
    ///</summary>
    ///
    private void MoveVertical(float dir)
    {
        transform.Translate(new Vector3(rigid.velocity.x, 0, dir) * speed * Time.deltaTime);
    }

    /// <summary>
    /// 플레이어 가로 이동 함수
    ///</summary>
    private void MoveHorizontal(float dir)
    {
        transform.Translate(new Vector3(dir, 0, rigid.velocity.z) * speed * Time.deltaTime);
    }

    /// <summary>
    /// 경계영역 지정 
    ///</summary>
    private void RimitPosition()
    {
        rimitMinZ = (createTrain.curTrainCount * -26) + 5;
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, rimitMinX, rimitMaxX), transform.position.y, Mathf.Clamp(transform.position.z, rimitMinZ, rimitMaxZ));
    }

    /// <summary>
    /// 플레이어 상호작용 범위 
    ///</summary>
    private void OnDrawGizmos()
    {
        Vector3 bias = transform.forward;
        Vector3 pos = transform.position;

        Gizmos.color = Color.green;

        for (int i = -360; i <= 360; i += 10)
        {
            Vector3 dir = Quaternion.Euler(0, -i, 0) * bias;
            Gizmos.DrawRay(pos, dir * playerVision);
        }

    }

    IEnumerator IsAttacking()
    {
        yield return new WaitForSeconds(1.25f);
        playerAnimation.SetAttack(false);
    }
}