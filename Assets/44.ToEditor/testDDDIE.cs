using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testDDDIE : MonoBehaviour
{
    private void OnDisable()
    {
        EngineSpawn.Instance.stateMobs.Remove(this.gameObject);
    }
}
