using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDrill : Enemy, IEnemyAttack
{
    public Animator anim;

    public Animator drillAnim;

    [SerializeField]
    private Collider drillCol;

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
        AnimationState(!run && anim.GetBool("IsAttack"));
        drillCol.enabled = anim.GetBool("IsAttack");
    }

    public void Attack(Quaternion rot)
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
    }

    public float GetDamage()
    {
        return damage;
    }
}