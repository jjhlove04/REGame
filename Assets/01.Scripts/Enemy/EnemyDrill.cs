using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDrill : Enemy
{
    public Animator drillAnim;

    [SerializeField]
    private Collider drillCol;

    protected override void OnEnable()
    {
        base.OnEnable();
        EnemyGetRandom();
    }

    protected override void Update()
    {
        base.Update();
        AnimationState(!run);
        drillCol.enabled = anim.GetBool("IsAttack");
    }

    protected override void Attack(Quaternion rot)
    {
        EnemyWaistLookForward();

        if (transform.position.x > 0)
        {
            rot = Quaternion.Euler(0, -90, 0);
        }

        else if (transform.position.x < 0)
        {
            rot = Quaternion.Euler(0, 90, 0);
        }

        AnimationState(true);

        if (target != trainManager.trainContainer[enemyType].transform)
        {
            rot = Quaternion.LookRotation(target.position - transform.position);
        }


        transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.deltaTime * 5);
    }

    private void AnimationState(bool value)
    {
        anim.SetBool("IsAttack", value);
        drillAnim.SetBool("IsAttack", value);
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
        PlayDieAnimationFalse();
    }

    public float GetDamage()
    {
        return enemyStat.damage;
    }

    public override void PlayDieAnimationTrue()
    {
        base.PlayDieAnimationTrue();
        drillAnim.SetBool("IsAttack", false);
    }    
    
    protected override void PlayDieAnimationFalse()
    {
        base.PlayDieAnimationFalse();
        drillAnim.SetBool("IsAttack", false);
    }
}