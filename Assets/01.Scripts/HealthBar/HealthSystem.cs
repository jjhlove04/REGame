using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HealthSystem : MonoBehaviour
{
    public Action OnDamaged;
    public Action OnDied;


    [SerializeField]
    private int healthAmountMax;

    private int curHealthAmount;

    private void Awake()
    {
        SetHealthAmountMax(healthAmountMax, true);
        InitHealth();
        OnDied += InitHealth;
    }

    public void Damage(int damageAmount)
    {
        curHealthAmount -= damageAmount;
        curHealthAmount = Mathf.Clamp(curHealthAmount, 0, healthAmountMax);

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
        return curHealthAmount == healthAmountMax;
    }

    public int GetHealthAmount()
    {
        return curHealthAmount;
    }

    public float GetHealthAmounetNomalized()
    {
        return (float)curHealthAmount / healthAmountMax;
    }

    public void SetHealthAmountMax(int hpAmountMax, bool updateHpAmount)
    {
        this.healthAmountMax = hpAmountMax;
        if (updateHpAmount)
        {
            curHealthAmount = hpAmountMax;
        }
    }

    private void InitHealth()
    {
        curHealthAmount = healthAmountMax;
    }
}