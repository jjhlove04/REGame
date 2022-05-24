using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainUpgrade : MonoBehaviour
{
    public void TrainCountUpgrade()
    {
        TestTurretDataBase.Instance.trainCount++;
    }
}
