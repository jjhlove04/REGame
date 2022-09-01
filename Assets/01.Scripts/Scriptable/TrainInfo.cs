using UnityEngine;

[CreateAssetMenu(fileName = "TrainInfo", menuName = "Scriptable Object/TrainInfo", order = int.MaxValue)]
public class TrainInfo : ScriptableObject
{
    public int trainCount;
    public int trainMaxShield;
    public int trainMaxHp;

    [Header("��ž ���׷��̵� ��")]

    public int trainCountPrice;
    public int trainCountPriceUp;

    public int hpUpgrade;
    public int hpPrice;
    public int hpPriceUp;

    public int shieldUpgrade;
    public int shieldPrice;
    public int shieldPriceUp;
}
