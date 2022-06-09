using System.Collections;
using UnityEngine;

public class EnemyRoketlancher : Enemy
{
    private float atime = 5f;

    private bool isAttack = false;

    [SerializeField]
    private Transform pos;

    private ObjectPool objPool;

    protected override void OnEnable()
    {
        base.OnEnable();
        EnemyGetRandom();
    }

    private void Start()
    {
        objPool = FindObjectOfType<ObjectPool>();
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void Attack(Quaternion rot)
    {
        EnemyWaistLookForward();

        Vector3 trainPos = TrainManager.instance.trainContainer[enemyType].transform.position;
        if (anim.GetBool("IsAttack"))
        {
            StartCoroutine(Attacking());

            if (target != trainManager.trainContainer[enemyType].transform)
            {
                rot = Quaternion.LookRotation(target.position - transform.position);
            }

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

        atime += Time.deltaTime;

        if (atime >= enemyStat.animTime + enemyStat.sAttackTime)
        {
            anim.SetBool("IsAttack", true);
            atime = 0f;
        }
    }

    private IEnumerator Attacking()
    {
        isAttack = true;
        yield return new WaitForSeconds(0.35f);
        SpawnBullet();
        yield return new WaitForSeconds(0.4f);
        isAttack = false;
    }

    private void SpawnBullet()
    {
        GameObject bullet = objPool.GetObject(Resources.Load<GameObject>("Missile"));
        bullet.GetComponent<Roketlancher>().Create(pos.position, target, enemyStat.damage);
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

    public override void PlayDieAnimationTrue()
    {
        base.PlayDieAnimationTrue();

        CameraManager.Instance.Shake(0.15f,1);
    }
}