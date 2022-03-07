using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHumanoidRig : MonoBehaviour, IEnemyAttack
{
    public Animator anim;

    private Enemy enemy;

    private Vector3 target;

    private void Start()
    {
        enemy = GetComponent<Enemy>();
    }

    private void Update()
    {
        if (enemy.run && anim.GetBool("IsAttack"))
        {
            anim.SetBool("IsAttack", false);
        }
    }

    public void Attack(Quaternion rot)
    {
        anim?.SetBool("IsAttack", true);

        target = TrainManager.instance.trainContainer[enemy.enemyType].transform.position - transform.position;
        rot = Quaternion.LookRotation(target);

        transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.deltaTime * 5);
    }
}
