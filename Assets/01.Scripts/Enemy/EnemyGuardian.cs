using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGuardian : Enemy
{
    private float atime = 2f;

    protected override void Attack(Quaternion rot)
    {
        if (transform.position.x > 0)
        {
            rot = Quaternion.Euler(0, -90, 0);
        }

        else if (transform.position.x < 0)
        {
            rot = Quaternion.Euler(0, 90, 0);
        }

        transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.deltaTime * 5);

        AnimationState(false);

        atime += Time.deltaTime;

        if (atime >= enemyStat.animTime + enemyStat.sAttackTime)
        {
            AnimationState(true);
            atime = 0f;
        }
    }

    private void AnimationState(bool value)
    {
        anim.SetBool("IsAttack", value);
    }

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

    protected override void EnemyGetRandom()
    {
        base.EnemyGetRandom();
    }

    /*private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Train"))
        {
            TrainScript.instance.Damage(enemyStat.damage * Time.deltaTime);
            TrainScript.instance.transform.parent.GetComponent<TrainHit>().Hit();
        }

        else if (other.CompareTag("Turret"))
        {
            other.GetComponent<HealthSystem>()?.Damage(enemyStat.damage * Time.deltaTime);
        }
    }*/

    public override void PlayDieAnimationTrue()
    {
        base.PlayDieAnimationTrue();
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, 0), Time.deltaTime * 5);
    }

    protected override void PlayDieAnimationFalse()
    {
        base.PlayDieAnimationTrue();
    }
}
