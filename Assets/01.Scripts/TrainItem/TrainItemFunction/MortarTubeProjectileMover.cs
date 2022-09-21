using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MortarTubeProjectileMover : MonoBehaviour
{
    [SerializeField]
    private float m_MaxDistance;

    [SerializeField]
    private LayerMask m_Mask;

    private float damage;

    private Rigidbody rigidbody;

    [SerializeField]
    private GameObject explosionEffect;

    private CameraManager cameraManager;


    private void OnEnable()
    {
        if (rigidbody != null)
        {
            transform.rotation = Quaternion.Euler(new Vector3(Random.Range(-80, -10), Random.Range(60, 120), 0));

            rigidbody.velocity = transform.forward * Random.Range(10, 50);
        }
    }

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();

        transform.rotation = Quaternion.Euler(new Vector3(Random.Range(-80, -10), Random.Range(50, 130), 0));

        rigidbody.velocity = transform.forward * Random.Range(10, 50);

        cameraManager = CameraManager.Instance;
    }

    protected void OnTriggerEnter(Collider other)
    {
        ObjectPool.instacne.GetObject(explosionEffect).transform.position = transform.position;

        gameObject.SetActive(false);

        WideAreaAttack();
    }

    private void WideAreaAttack()
    {
        Collider[] collider = Physics.OverlapSphere(transform.position, m_MaxDistance, m_Mask);

        cameraManager.Shake(0.25f, 1);

        HealthSystem healthSystem = new HealthSystem();

        if (collider != null)
        {

            for (int i = 0; i < collider.Length; i++)
            {
                if (collider[i].gameObject.activeSelf)
                {
                    healthSystem = collider[i].GetComponent<HealthSystem>();

                    healthSystem?.WideAreaDamge(damage);
                }
            }
        }
    }

    public MortarTubeProjectileMover Create(float damage)
    {
        this.damage = damage;

        return this;
    }

}
