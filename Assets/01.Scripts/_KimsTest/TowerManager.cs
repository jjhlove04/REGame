using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
    public void Buy(GameObject tower)
    {
        TestTurretDataBase.Instance.towerObj = tower;
    }

    public void Tower1()
    {
        
    }
}
