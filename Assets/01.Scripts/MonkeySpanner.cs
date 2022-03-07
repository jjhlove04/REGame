using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MonkeySpanner : MonoBehaviour
{
    private Rigidbody rigid;
    public float speed = 1f;
    public float damage=5;

    private Vector3 lastMoveDir;
    Vector3 forceDirection;

    private float curLifeTime = 0f;
    public float lifeTime = 2f;
    private void OnEnable()
    {
        var p = FindObjectOfType<PlayerInput>();
        p.fireBullet.AddListener(Fire);
    }
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }



    void Update()
    {
        curLifeTime += Time.deltaTime;
        // 이동
        float moveSpeed = 20f;
        transform.position += forceDirection * moveSpeed * Time.deltaTime;

        ////회전값 적용
        //transform.LookAt(targetEnemy);
    }

    public void Fire()
    {

        if (curLifeTime > lifeTime)
        {
            curLifeTime = 0f;
            this.gameObject.SetActive(false);
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);


        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.gameObject.GetComponent<Enemy>() != null)
            {
                Vector3 distanceToTarget = hit.point - transform.position;
                forceDirection = distanceToTarget.normalized;
                lastMoveDir = forceDirection;
            }
            else
            {
                forceDirection = lastMoveDir;
            }
        }



    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            other.GetComponent<HealthSystem>().Damage(damage);
            this.gameObject.SetActive(false);
        }
    }


}