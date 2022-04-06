using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFireAttack : Enemy, IEnemyAttack
{
    private float atime = 5f;

    private bool isAttack = false;

    protected override void OnEnable()
    {
        base.OnEnable();
        EnemyGetRandom();
    }

    protected override void Start()
    {
        base.Start();
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
            StartCoroutine(Attacking());
            transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.deltaTime * 5);
        }

        if (isAttack)
        {
            if (transform.position.x > 0)
            {
                rot = Quaternion.Euler(0, -60, 0);
            }

            else if (transform.position.x < 0)
            {
                rot = Quaternion.Euler(0, 60, 0);
            }
            transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.deltaTime * 5);
        }

        else
        {
            rot = Quaternion.LookRotation(Vector3.zero);
            transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.deltaTime * 5);
        }

        anim.SetBool("IsAttack", false);

        enemyStat.animTime += Time.deltaTime;

        if (enemyStat.animTime >= atime+ enemyStat.sAttackTime)
        {
            anim.SetBool("IsAttack", true);
            enemyStat.animTime = 0f;
        }
    }

    private IEnumerator Attacking()
    {
        isAttack = true;
        yield return new WaitForSeconds(1f* enemyStat.sAttackTime);
        isAttack = false;
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

    public float GetDamage()
    {
        return enemyStat.damage;
    }

    public override void PlayDieAnimationTrue()
    {
        base.PlayDieAnimationTrue();
    }

    protected override void PlayDieAnimationFalse()
    {
        base.PlayDieAnimationTrue();
    }
}
