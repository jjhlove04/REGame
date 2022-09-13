using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BumperGrapplerLinoleum : MonoBehaviour
{
    [SerializeField]
    private float m_MaxDistance;

    public LayerMask m_Mask;

    private float lifeTime;

    private ObjectPool objectPool;

    private void OnEnable()
    {
        objectPool = ObjectPool.instacne;

        Invoke("ObjReturn", lifeTime);
    }

    private void Start()
    {
        transform.localScale = new Vector3(m_MaxDistance, 0.001f, m_MaxDistance);
    }

    private void Update()
    {
        BumperGrapplerleumAttack();
    }
    private void ObjReturn()
    {
        objectPool.ReturnGameObject(this.gameObject);
    }

    public void Create(float lifeTime)
    {
        this.lifeTime = lifeTime;
    }

    private void BumperGrapplerleumAttack()
    {
        Collider[] collider = Physics.OverlapSphere(transform.position, m_MaxDistance, m_Mask);

        Enemy enemy = new Enemy();

        for (int i = 0; i < collider.Length; i++)
        {
            if (collider[i].gameObject.activeSelf)
            {
                enemy = collider[i].GetComponent<Enemy>();

                enemy.OnBumperGrappler();
            }
        }
    }
}
