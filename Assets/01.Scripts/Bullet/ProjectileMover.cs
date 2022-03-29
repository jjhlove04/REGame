using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMover : MonoBehaviour
{
    public float speed = 15f;
    public float hitOffset = 0f;
    public bool UseFirePointRotation;
    public Vector3 rotationOffset = new Vector3(0, 0, 0);
    public GameObject hit;
    public GameObject flash;
    private Rigidbody rb;
    public GameObject[] Detached;

    // 이동
    public float moveSpeed = 50;


    private Transform targetEnemy;
    private Vector3 lastMoveDir;
    private int damage;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (flash != null)
        {
            var flashInstance = Instantiate(flash, transform.position, Quaternion.identity);
            flashInstance.transform.forward = gameObject.transform.forward;
            var flashPs = flashInstance.GetComponent<ParticleSystem>();
            if (flashPs != null)
            {
                Destroy(flashInstance, flashPs.main.duration);
            }
            else
            {
                var flashPsParts = flashInstance.transform.GetChild(0).GetComponent<ParticleSystem>();
                Destroy(flashInstance, flashPsParts.main.duration);
            }
        }
        Destroy(gameObject,5);
	}

    void FixedUpdate ()
    {
        Vector3 moveDir;
        if (targetEnemy != null && !targetEnemy.GetComponent<Enemy>().isDying)
        {
            moveDir = (targetEnemy.transform.position - transform.position).normalized;
            lastMoveDir = moveDir;
        }

        else
        {
            moveDir = lastMoveDir;
        }

        // 이동
        transform.position += moveDir * moveSpeed * Time.deltaTime;

        //회전값 적용
        transform.LookAt(targetEnemy);
    }

    //https ://docs.unity3d.com/ScriptReference/Rigidbody.OnCollisionEnter.html
    void OnCollisionEnter(Collision collision)
    {
        //Lock all axes movement and rotation

    }

    public void Create(Transform enemy, int damage)
    {
        SetTarget(enemy);
        Damage(damage);
    }

    void SetTarget(Transform enemy)
    {
        this.targetEnemy = enemy;
    }

    public void Damage(int damage)
    {
        this.damage = damage;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            rb.constraints = RigidbodyConstraints.FreezeAll;

            other.GetComponent<HealthSystem>().Damage(damage);

            if (hit != null)
            {
                var hitInstance = Instantiate(hit);
                hitInstance.transform.position = transform.position + new Vector3(2,0,0);

                var hitPs = hitInstance.GetComponent<ParticleSystem>();
                if (hitPs != null)
                {
                    Destroy(hitInstance, hitPs.main.duration);
                }
                else
                {
                    var hitPsParts = hitInstance.transform.GetChild(0).GetComponent<ParticleSystem>();
                    Destroy(hitInstance, hitPsParts.main.duration);
                }
            }
            foreach (var detachedPrefab in Detached)
            {
                if (detachedPrefab != null)
                {
                    detachedPrefab.transform.parent = null;
                }
            }
            gameObject.SetActive(false);
            //DamageText.Create(targetEnemy.position, damage,new Color(1,42/255,42/255));
        }
    }
}
