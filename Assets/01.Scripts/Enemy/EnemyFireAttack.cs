using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFireAttack : MonoBehaviour, IEnemyAttack
{
    public Animator anim;

    public float animTime;
    private float atime = 5f;

    private bool isAttack = false;

    public void Attack(Quaternion rot)
    {
        if (anim.GetBool("IsAttack"))
        {
            StartCoroutine(Attacking());
            transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.deltaTime * 5);
        }

        if (isAttack)
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
        yield return new WaitForSeconds(2f);
        isAttack = false;
    }
}
