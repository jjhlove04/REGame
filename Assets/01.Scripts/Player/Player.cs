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
    private GameObject upperBody, lowerBody;

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

    private float followMaxTime = 3;
    private float followTime;

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
        followTime = followMaxTime;
    }

    private void Update()
    {
        upperBody.transform.localPosition = Vector3.zero;
        lowerBody.transform.localPosition = Vector3.zero;
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
            followTime = followMaxTime;
            horizontalDir = Input.GetAxis("Horizontal");

            MoveHorizontal(horizontalDir);
            if (horizontalDir > 0)
            {
                if (lowerBody.transform.eulerAngles.y >= -45 && lowerBody.transform.eulerAngles.y < 45)
                {
                    playerAnimation.SetSide(true);
                    playerAnimation.IsRight();
                }

                else if (lowerBody.transform.eulerAngles.y >= 45 && lowerBody.transform.eulerAngles.y < 135)
                {
                    playerAnimation.SetMove(true);
                    playerAnimation.IsForward();
                }

                else if (lowerBody.transform.eulerAngles.y >= 135 && lowerBody.transform.eulerAngles.y < 225)
                {
                    playerAnimation.SetSide(true);
                    playerAnimation.Isleft();
                }

                else if (lowerBody.transform.eulerAngles.y >= 225 && lowerBody.transform.eulerAngles.y < 315)
                {
                    playerAnimation.SetMove(true);
                    playerAnimation.IsBack();
                }
            }

            else if (horizontalDir < 0)
            {
                if (lowerBody.transform.eulerAngles.y >= -45 && lowerBody.transform.eulerAngles.y < 45)
                {
                    playerAnimation.SetSide(true);
                    playerAnimation.Isleft();
                }

                else if (lowerBody.transform.eulerAngles.y >= 45 && lowerBody.transform.eulerAngles.y < 135)
                {
                    playerAnimation.SetMove(true);
                    playerAnimation.IsBack();
                }

                else if (lowerBody.transform.eulerAngles.y >= 135 && lowerBody.transform.eulerAngles.y < 225)
                {
                    playerAnimation.SetSide(true);
                    playerAnimation.IsRight();
                }

                else if (lowerBody.transform.eulerAngles.y >= 225 && lowerBody.transform.eulerAngles.y < 315)
                {
                    playerAnimation.SetMove(true);
                    playerAnimation.IsForward();
                }
            }
        }

        if (Input.GetAxis("Vertical") != 0 && !playerAnimation.upperanimator.GetBool("Attack"))
        {
            followTime = followMaxTime;
            verticalDir = Input.GetAxis("Vertical");


            MoveVertical(verticalDir);
            if (verticalDir > 0)
            {
                if (lowerBody.transform.eulerAngles.y >= -45 && lowerBody.transform.eulerAngles.y < 45)
                {
                    playerAnimation.SetMove(true);
                    playerAnimation.IsForward();
                }

                else if (lowerBody.transform.eulerAngles.y >= 45 && lowerBody.transform.eulerAngles.y < 135)
                {
                    playerAnimation.SetSide(true);
                    playerAnimation.Isleft();
                }

                else if (lowerBody.transform.eulerAngles.y >= 135 && lowerBody.transform.eulerAngles.y < 225)
                {
                    playerAnimation.SetMove(true);
                    playerAnimation.IsBack();
                }

                else if (lowerBody.transform.eulerAngles.y >= 225 && lowerBody.transform.eulerAngles.y < 315)
                {
                    playerAnimation.SetSide(true);
                    playerAnimation.IsRight();
                }
            }

            else if (verticalDir < 0)
            {
                if (lowerBody.transform.eulerAngles.y >= -45 && lowerBody.transform.eulerAngles.y < 45)
                {
                    playerAnimation.SetMove(true);
                    playerAnimation.IsBack();
                }

                else if (lowerBody.transform.eulerAngles.y >= 45 && lowerBody.transform.eulerAngles.y < 135)
                {
                    playerAnimation.SetSide(true);
                    playerAnimation.IsRight();
                }

                else if (lowerBody.transform.eulerAngles.y >= 135 && lowerBody.transform.eulerAngles.y < 225)
                {
                    playerAnimation.SetMove(true);
                    playerAnimation.IsForward();
                }

                else if (lowerBody.transform.eulerAngles.y >= 225 && lowerBody.transform.eulerAngles.y < 315)
                {
                    playerAnimation.SetSide(true);
                    playerAnimation.Isleft();
                }
            }
        }

        if (Input.GetAxis("Vertical") == 0 && Input.GetAxis("Horizontal") == 0)
        {
            playerAnimation.Idle();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            followTime = followMaxTime;
            speed = Initspeed * runspeed;
            playerAnimation.SetRun(true);
        }

        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            followTime = followMaxTime;
            speed = Initspeed;
            playerAnimation.SetRun(false);
        }

        if (Input.GetMouseButtonDown(0))
        {
            followTime = followMaxTime;
            playerAnimation.SetAttack(true);
            lowerBody.transform.rotation = upperBody.transform.rotation;
            StartCoroutine(IsAttacking());
        }
    }
    ///<summary>
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
            float rot = Mathf.LerpAngle(upperBody.transform.eulerAngles.y, degree, Time.deltaTime * rotateSpeed);
            upperBody.transform.eulerAngles = new Vector3(0, rot, 0);
            if(rot > upperBody.transform.eulerAngles.y + 10)
            {
                followTime = followMaxTime;
            }

            FollowLowerRotate();
        }
    }

    private void FollowLowerRotate()
    {
        if(upperBody.transform.eulerAngles.y> lowerBody.transform.eulerAngles.y+55)
        {
            followTime = followMaxTime;
            lowerBody.transform.rotation = Quaternion.Lerp(lowerBody.transform.rotation, upperBody.transform.rotation, Time.deltaTime * rotateSpeed);
        }

        else if(upperBody.transform.eulerAngles.y < lowerBody.transform.eulerAngles.y - 55)
        {
            followTime = followMaxTime;
            lowerBody.transform.rotation = Quaternion.Lerp(lowerBody.transform.rotation, upperBody.transform.rotation, Time.deltaTime * rotateSpeed);
        }

        else
        {
            followTime-=Time.deltaTime;
            if(followTime < 0)
            {
                FollowRotate();
            }
        }

    }

    private void FollowRotate()
    {
        lowerBody.transform.rotation = Quaternion.Lerp(lowerBody.transform.rotation, upperBody.transform.rotation, Time.deltaTime * rotateSpeed);
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