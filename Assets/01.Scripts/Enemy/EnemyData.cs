using UnityEngine;

[CreateAssetMenu(fileName = "Enemy Data", menuName = "Scriptable Object/Enemy Data", order = int.MaxValue)]
public class EnemyData : ScriptableObject
{
    public GameObject enemy;

    public float enemySpeed = 10f;

    public float distanceX = 3;

    public float minDistance, maxDistance;

    public float damage;

    public float sAttackTime;

    public float animTime;

    public float healthAmountMax;

    public float def;

    public int dropExp;
    public int dropGold;

    public float GetDamage()
    {
        return damage * (1 / sAttackTime);
    }
}
