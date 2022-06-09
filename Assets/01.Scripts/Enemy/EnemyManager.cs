using System.Collections.Generic;
using UnityEngine;

public enum EnemyType
{
    Fire,
    Drill,
    Guardian,
    HumanoidRig,
    Roket
}

public class EnemyManager : MonoBehaviour
{
    private static EnemyManager instance;

    public static EnemyManager Instance{ get { return instance; }}

    int fireAttackCount;
    int drillAttackCount;
    int HumanoidRigAttackCount;
    int GuardianAttackCount;

    Dictionary<EnemyType, EnemyData> dicEnemydata = new Dictionary<EnemyType, EnemyData>();

    private void Awake()
    {
        instance = this;
        EnemyDataInit();
    }

    private void EnemyDataInit()
    {
        dicEnemydata.Add(EnemyType.Fire, Resources.Load<EnemyData>("Fire"));
        dicEnemydata.Add(EnemyType.Drill, Resources.Load<EnemyData>("Drill"));
        dicEnemydata.Add(EnemyType.Guardian, Resources.Load<EnemyData>("Guardian"));
        dicEnemydata.Add(EnemyType.HumanoidRig, Resources.Load<EnemyData>("HumanoidRig"));
        dicEnemydata.Add(EnemyType.Roket, Resources.Load<EnemyData>("Roket"));
    }

    public void AttackStart(EnemyType enemytype)
    {
        switch (enemytype)
        {
            case EnemyType.Fire:

                fireAttackCount++;
                break;

            case EnemyType.Drill:

                drillAttackCount++;
                break;

            case EnemyType.Guardian:

                GuardianAttackCount++;
                break;

            case EnemyType.HumanoidRig:

                HumanoidRigAttackCount++;
                break;

            case EnemyType.Roket:
                break;

            default:
                break;
        }
    }

    public void AttackEnd(EnemyType enemytype)
    {
        switch (enemytype)
        {
            case EnemyType.Fire:

                fireAttackCount--;
                break;

            case EnemyType.Drill:

                drillAttackCount--;
                break;

            case EnemyType.Guardian:

                GuardianAttackCount--;
                break;

            case EnemyType.HumanoidRig:

                HumanoidRigAttackCount--;
                break;

            case EnemyType.Roket:
                break;

            default:
                break;
        }
    }

    public float Damage()
    {
        return (fireAttackCount * dicEnemydata[EnemyType.Fire].damage * Time.deltaTime) +
            (HumanoidRigAttackCount * dicEnemydata[EnemyType.HumanoidRig].damage * Time.deltaTime) +
            (GuardianAttackCount * dicEnemydata[EnemyType.Guardian].damage * Time.deltaTime) +
            (drillAttackCount * dicEnemydata[EnemyType.Drill].damage * Time.deltaTime);
    }
}