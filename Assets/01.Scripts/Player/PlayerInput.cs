using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
//code.gondr.net => 87번 강의 
public class PlayerInput : MonoBehaviour
{
    public static PlayerInput Instance;
    private static PlayerInput _instnace
    {
        get { return Instance; }
    }

    private ObjectPool objPool;
    public GameObject firePrefab;
    public Transform firePos;

    public float cooldown;
    private float curCooldown;

    public string fireButtonName = "Fire1";

    public bool fire { get; private set; }
    public Vector3 mousePos { get; private set; }
    public LayerMask whatIsGround;

    public bool isEnemy;

    [HideInInspector]
    public UnityEvent fireBullet;

    public GameObject curTurret;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        objPool = FindObjectOfType<ObjectPool>();
    }

    void Update()
    {
        fire = Input.GetButtonDown(fireButtonName);

        //Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        ////mousePosition은 스크린좌표로 나온다. 
        ////따라서 스크린좌표로 레이를 쏘면 그 방향에 있는 월드좌표를 구할 수 있다.
        //RaycastHit hit;
        //float depth = Camera.main.farClipPlane; //f12적으면 유니티 함수들은 정의 파일을 볼 수 있다.
        //if (Physics.Raycast(camRay, out hit, depth, whatIsGround))
        //{
        //    mousePos = hit.point;
        //    // Debug.DrawRay(Camera.main.transform.position, 
        //    //             camRay.direction * depth,
        //    //             Color.red,
        //    //             0.5f);
        //}

        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        //mousePosition은 스크린좌표로 나온다. 
        //따라서 스크린좌표로 레이를 쏘면 그 방향에 있는 월드좌표를 구할 수 있다.
        RaycastHit hit;
        float depth = Camera.main.farClipPlane; //f12적으면 유니티 함수들은 정의 파일을 볼 수 있다.
        if (Physics.Raycast(camRay, out hit, depth, whatIsGround))
        {
            mousePos = hit.point;
            // Debug.DrawRay(Camera.main.transform.position, 
            //             camRay.direction * depth,
            //             Color.red,
            //             0.5f);
        }

        curCooldown += Time.deltaTime;

        Fire();
    }

    public void Fire() //
    {
        if (fire && isEnemy && curCooldown >= cooldown)
        {
            GameObject bullet = objPool.GetObject(firePrefab);
            bullet.transform.position = firePos.transform.position;
            bullet.transform.rotation = firePos.transform.rotation;

            fireBullet.Invoke();

            curCooldown = 0;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(mousePos, 0.5f);
        //마우스 위치에 0.5반지름으로 원을 그려준다.
    }
}