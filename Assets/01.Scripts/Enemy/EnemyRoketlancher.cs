using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRoketlancher : MonoBehaviour, IEnemyAttack
{
    public Animator anim;

    public float animTime;
    private float atime = 5f;

    private bool isAttack = false;

    [SerializeField]
    private int damage;

    [SerializeField]
    private Transform pos;


    private Enemy enemy;

    private Vector3 target;

    private void Start()
    {
        enemy = GetComponent<Enemy>();
    }

    public void Attack(Quaternion rot)
    {
        if (anim.GetBool("IsAttack"))
        {
            StartCoroutine(Attacking());
            transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.deltaTime * 5);
        }

        if (isAttack)
        {
            target = TrainManager.instance.trainContainer[enemy.enemyType].transform.position-transform.position ;
            rot = Quaternion.LookRotation(target);

            transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.deltaTime * 5);
        }

        else
        {
            rot = Quaternion.LookRotation(Vector3.zero);
            transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.deltaTime * 5);
        }

        anim.SetBool("IsAttack", false);

        animTime += Time.deltaTime;

        if (animTime >= atime)
        {
            anim.SetBool("IsAttack", true);
            animTime = 0f;
        }
    }

    private IEnumerator Attacking()
    {
        isAttack = true;
        yield return new WaitForSeconds(0.35f);
        Roketlancher.Create(pos.position, TrainManager.instance.trainContainer[enemy.enemyType].transform, damage);
        yield return new WaitForSeconds(0.4f);
        isAttack = false;
    }
}