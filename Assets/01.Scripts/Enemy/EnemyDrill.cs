using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDrill : Enemy, IEnemyAttack
{
    public Animator anim;

    public Animator drillAnim;
    
    private Enemy enemy;

    [SerializeField]
    private Collider drillCol;

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    protected override void Start()
    {
        base.Start();
        enemy = GetComponent<Enemy>();
    }

    protected override void Update()
    {
        base.Update();
        AnimationState(!enemy.run && anim.GetBool("IsAttack"));
        drillCol.enabled = anim.GetBool("IsAttack");
    }

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

        AnimationState(true);

        transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.deltaTime * 5);
    }

    private void AnimationState(bool value)
    {
        anim.SetBool("IsAttack", value);
        drillAnim.SetBool("IsAttack", value);
    }
}