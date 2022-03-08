using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDrill : MonoBehaviour, IEnemyAttack
{
    public Animator anim;

    public Animator drillAnim;
    
    private Enemy enemy;

    [SerializeField]
    private Collider drillCol;

    private void Start()
    {
        enemy = GetComponent<Enemy>();
    }

    private void Update()
    {
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