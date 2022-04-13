using System.Collections;
using System.Collections.Generic;
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
    }

    private void Start()
    {
        InitHealth();
        SetHealthAmountMax(enemy.enemyStat.healthAmountMax, true);
        OnDied.AddListener(InitHealth);
    }

    private void OnDisable()
    {
        IsFullHealth();
    }

    public void Damage(float damageAmount)
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
}