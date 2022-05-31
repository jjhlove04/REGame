using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BaitHealthSystem : MonoBehaviour
{
    public Action OnDamaged;
    public UnityEvent OnDied;

    private float curHealthAmount;

    public int maxHp;

    private void Awake()
    {
        InitHealth();

        OnDied.AddListener(InitHealth);
    }

    private void Start()
    {
        SetHealthAmountMax(maxHp, true);
    }


    public bool IsDead()
    {
        return curHealthAmount == 0;
    }


    public bool IsFullHealth()
    {
        return curHealthAmount == maxHp;
    }

    public float GetHealthAmount()
    {
        return curHealthAmount;
    }

    public float GetHealthAmounetNomalized()
    {
        return (float)curHealthAmount / maxHp;
    }

    public void SetHealthAmountMax(float hpAmountMax, bool updateHpAmount)
    {
        if (updateHpAmount)
        {
            curHealthAmount = hpAmountMax;
        }
    }

    public void InitHealth()
    {
        curHealthAmount = maxHp;
    }

    public void Damage(float damageAmount)
    {
        curHealthAmount -= damageAmount;
        curHealthAmount = Mathf.Clamp(curHealthAmount, 0, maxHp);
        transform.GetChild(0).GetComponentInChildren<EnemyColorChange>()?.Hit();
        OnDamaged?.Invoke();

        if (IsDead())
        {
            OnDied?.Invoke();
        }
    }
}
