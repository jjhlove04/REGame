using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Rigidbody rigid;

    [SerializeField]
    private float rimitMinX, rimitMaxX;

    [SerializeField]
    private float rimitMinZ, rimitMaxZ;


    [SerializeField]
    private float speed;

    float horizontalDir;
    float verticalDir;

    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetAxis("Horizontal") != 0)
        {
            horizontalDir = Input.GetAxis("Horizontal");

            MoveHorizontal(horizontalDir);
        }

        if (Input.GetAxis("Vertical") != 0)
        {
            verticalDir = Input.GetAxis("Vertical");

            MoveVertical(verticalDir);
        }

        RimitPosition();
    }

    private void MoveVertical(float dir)
    {
        rigid.velocity = new Vector3(rigid.velocity.x, 0, dir * speed);
    }

    private void MoveHorizontal(float dir)
    {
        rigid.velocity = new Vector3(dir * speed, 0, rigid.velocity.z);
    }

    private void RimitPosition()
    {
        rimitMinZ = (TrainManager.instance.curTrainCount * -20) + 10; //숫자는 기차 사이즈에 따라 바뀌어야함
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, rimitMinX, rimitMaxX), transform.position.y, Mathf.Clamp(transform.position.z, rimitMinZ, rimitMaxZ));
    }
}