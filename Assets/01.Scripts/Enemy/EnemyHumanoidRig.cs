using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHumanoidRig : Enemy, IEnemyAttack
{
    private Vector3 target;

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
        if (run && anim.GetBool("IsAttack"))
        {
            anim.SetBool("IsAttack", false);
        }
    }

    public void Attack(Quaternion rot)
    {
        EnemyWaistLookForward();

        anim?.SetBool("IsAttack", true);

        target = TrainManager.instance.trainContainer[enemyType].transform.position - transform.position;
        rot = Quaternion.LookRotation(target);

        transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.deltaTime * 5);
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
