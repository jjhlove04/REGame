using System.Collections;
using UnityEngine;
using System;
using UnityEngine.Events;

public class HealthSystem : MonoBehaviour
{
    public Action OnDamaged;
    public UnityEvent OnDied;

    private Enemy enemy;

    private float curHealthAmount;

    private void Awake()
    {
        enemy = GetComponent<Enemy>();
        OnDied.AddListener(InitHealth);
        InitHealth();
    }

    private void Start()
    {
        SetHealthAmountMax(enemy.enemyStat.healthAmountMax, true);
    }

    public bool IsDead()
    {
        return curHealthAmount == 0;
    }


    public bool IsFullHealth()
    {
        return curHealthAmount == enemy.enemyStat.healthAmountMax;
    }

    public float GetHealthAmount()
    {
        return curHealthAmount;
    }

    public float GetHealthAmounetNomalized()
    {
        return (float)curHealthAmount / enemy.enemyStat.healthAmountMax;
    }

    public void SetHealthAmountMax(float hpAmountMax, bool updateHpAmount)
    {
        this.enemy.enemyStat.healthAmountMax = hpAmountMax;
        if (updateHpAmount)
        {
            curHealthAmount = hpAmountMax;
        }
    }

    public void InitHealth()
    {
        curHealthAmount = enemy.enemyStat.healthAmountMax;
    }

    public void WideAreaDamge(float damage)
    {
        if(enemy.onlyDamage != OnlyDamage.singular && !enemy.isDying)
        {
            Damage(damage);
        }
    }

    public void Damage(float damageAmount)
    {
        if(enemy.onlyDamage != OnlyDamage.wideArea && !enemy.isDying)
        {
            curHealthAmount -= damageAmount;
            curHealthAmount = Mathf.Clamp(curHealthAmount, 0, enemy.enemyStat.healthAmountMax);
            transform.GetChild(0).GetComponentInChildren<EnemyColorChange>()?.Hit();
            OnDamaged?.Invoke();

            if (IsDead())
            {
                OnDied?.Invoke();
            }
        }
    }

    public void DotDamageCoroutine(GameObject particle, int dotcount, float dotDelay, float dotDamage)
    {
        if (enemy.onlyDamage != OnlyDamage.wideArea && !enemy.isDying)
        {
            StartCoroutine(DotDamage(particle, dotcount, dotDelay, dotDamage));
        }
    }

    IEnumerator DotDamage(GameObject particle, int dotcount, float dotDelay, float dotDamage)
    {
        GameObject particleObj = ObjectPool.instacne.GetObject(particle);

        particleObj.transform.parent = transform;

        for (int i = 0; i < dotcount; i++)
        {
            if(curHealthAmount > 0)
            {
                Damage(dotDamage);

                yield return new WaitForSeconds(dotDelay);
            }
        }

        particleObj.transform.parent = null;
        particle.transform.position = Vector3.zero;
        particleObj.SetActive(false);
    }
}