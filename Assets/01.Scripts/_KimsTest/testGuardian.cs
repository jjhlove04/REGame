using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testGuardian : TestEnemy, IEnemyAttack
{
    public float animTime;
    private float atime = 2f;

    public void Attack(Quaternion rot)
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

        animTime += Time.deltaTime;

        if (animTime >= atime)
        {
            AnimationState(true);
            animTime = 0f;
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

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Train")
        {
            TrainScript.instance.Damage(damage * Time.deltaTime);
            other.GetComponent<TrainHit>()?.Hit();
        }

        else if (other.tag == "Turret")
        {
            other.GetComponent<HealthSystem>()?.Damage(damage * Time.deltaTime);
        }
    }

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
