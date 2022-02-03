using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
//code.gondr.net => 87�� ���� 
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
        ////mousePosition�� ��ũ����ǥ�� ���´�. 
        ////���� ��ũ����ǥ�� ���̸� ��� �� ���⿡ �ִ� ������ǥ�� ���� �� �ִ�.
        //RaycastHit hit;
        //float depth = Camera.main.farClipPlane; //f12������ ����Ƽ �Լ����� ���� ������ �� �� �ִ�.
        //if (Physics.Raycast(camRay, out hit, depth, whatIsGround))
        //{
        //    mousePos = hit.point;
        //    // Debug.DrawRay(Camera.main.transform.position, 
        //    //             camRay.direction * depth,
        //    //             Color.red,
        //    //             0.5f);
        //}

        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        //mousePosition�� ��ũ����ǥ�� ���´�. 
        //���� ��ũ����ǥ�� ���̸� ��� �� ���⿡ �ִ� ������ǥ�� ���� �� �ִ�.
        RaycastHit hit;
        float depth = Camera.main.farClipPlane; //f12������ ����Ƽ �Լ����� ���� ������ �� �� �ִ�.
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
        //���콺 ��ġ�� 0.5���������� ���� �׷��ش�.
    }
}