using System.Collections;
using UnityEngine;
using System;
using UnityEngine.Events;

public class HealthSystem : MonoBehaviour
{
    public GameObject flaotingText;
    public Action OnDamaged;
    public UnityEvent OnDied;

    private Enemy enemy;

    private float curHealthAmount;

    private bool onEngineOil = false;

    private float curEngineOilTime;
    private float maxEngineOilTime;

    private float damage;

    private float maxTime = 0;

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

    private void Update()
    {
        EngineOilLinoleumDamage();
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
            if (enemy.onFurryBracelet)
            {
            curHealthAmount -= ((damageAmount) * (100 / (100 + enemy.enemyStat.def)))*0.5f;
            }  
            var damageTxt = Instantiate(flaotingText, transform.position,Quaternion.Euler(50,-90,0));
            damageTxt.GetComponent<TextMesh>().text = damageAmount.ToString();
            curHealthAmount -= (damageAmount) * (100 / (100 + enemy.enemyStat.def));
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

        particle.transform.position = Vector3.zero;

        for (int i = 0; i < dotcount; i++)
        {
            if(curHealthAmount > 0)
            {
                Damage(dotDamage);

                yield return new WaitForSeconds(dotDelay);
            }
        }


        particleObj.SetActive(false);
        particleObj.transform.parent = null;
    }

    public void FMJ(float damageAmount, float additionalDamage)
    {
        if (curHealthAmount > enemy.enemyStat.healthAmountMax * 0.8f)
        {
            damageAmount *= 1.25f;
        }

        Damage(damageAmount);
    }

    public void FurryBracelet(float time)
    {
        enemy.OnFurryBracelet(time);
    }

    public void OnEngineOil(float dotMaxTime, float dotDelay, float dotDamage)
    {
        maxTime = dotMaxTime;

        maxEngineOilTime = dotDelay;

        damage = dotDamage;
    }

    private void EngineOilLinoleumDamage()
    {
        if (maxTime > 0)
        {
            if (maxEngineOilTime >= curEngineOilTime)
            {
                maxTime -= maxEngineOilTime;

                curEngineOilTime = 0;

                Damage(damage * (maxEngineOilTime / maxTime));
            }
        }
    }
}