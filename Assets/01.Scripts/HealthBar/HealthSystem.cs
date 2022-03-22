using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

public class HealthSystem : MonoBehaviour
{
    public Action OnDamaged;
    public UnityEvent OnDied;

    [SerializeField]
    private float healthAmountMax;

    [SerializeField]
    private float curHealthAmount;

    private void Awake()
    {
        SetHealthAmountMax(healthAmountMax, true);
        InitHealth();
        OnDied.AddListener(InitHealth);
    }

    private void Start()
    {
        OnDied?.Invoke();
    }

    public void Damage(float damageAmount)
    {
        curHealthAmount -= damageAmount;
        curHealthAmount = Mathf.Clamp(curHealthAmount, 0, healthAmountMax);
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
        return curHealthAmount == healthAmountMax;
    }

    public float GetHealthAmount()
    {
        return curHealthAmount;
    }

    public float GetHealthAmounetNomalized()
    {
        return (float)curHealthAmount / healthAmountMax;
    }

    public void SetHealthAmountMax(float hpAmountMax, bool updateHpAmount)
    {
        this.healthAmountMax = hpAmountMax;
        if (updateHpAmount)
        {
            curHealthAmount = hpAmountMax;
        }
    }

    public void InitHealth()
    {
        curHealthAmount = healthAmountMax;
    }
}