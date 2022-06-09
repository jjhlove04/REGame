using System.Collections.Generic;
using UnityEngine;
using MEC;

public class testRoketlancher : TestEnemy, IEnemyAttack
{
    public float animTime;
    private float atime = 5f;

    private bool isAttack = false;

    [SerializeField]
    private Transform pos;

    private ObjectPool objPool;

    private Vector3 target;

    protected override void OnEnable()
    {
        base.OnEnable();
        EnemyGetRandom();
    }

    protected override void Start()
    {
        base.Start();
        objPool = FindObjectOfType<ObjectPool>();
    }

    protected override void Update()
    {
        base.Update();
    }


    public void Attack(Quaternion rot)
    {
        EnemyWaistLookForward();

        if (anim.GetBool("IsAttack"))
        {
            //StartCoroutine(Attacking());
            Timing.RunCoroutine(Attacking());
            transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.deltaTime * 5);
        }

        /* if (isAttack)
         {
             target = new Vector3(trainPos.x, trainPos.y, trainPos.z+10) - transform.position ;
             rot = Quaternion.LookRotation(target);

             transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.deltaTime * 5);
         }*/

        else
        {
            rot = Quaternion.LookRotation(Vector3.zero);
            transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.deltaTime * 5);
        }

        anim.SetBool("IsAttack", false);

        animTime += Time.deltaTime;

        if (animTime >= atime)
        {
            anim.SetBool("IsAttack", true);
            animTime = 0f;
        }
    }

    private IEnumerator<float> Attacking()
    {
        isAttack = true;
        yield return Timing.WaitForSeconds(0.35f);
        SpawnBullet();
        yield return Timing.WaitForSeconds(0.4f);
        isAttack = false;
    }

    private void SpawnBullet()
    {
        GameObject bullet = objPool.GetObject(Resources.Load<GameObject>("Missile"));
        bullet.GetComponent<Roketlancher>().Create(pos.position, dir2, damage);
    }

    protected override void EnemyGetRandom()
    {
        base.EnemyGetRandom();
        EnemyWaistInit();
    }

    protected override void EnemyWaistLookForward()
    {
        base.EnemyWaistLookForward();
    }

    protected override void EnemyWaistInit()
    {
        base.EnemyWaistInit();
    }
}
