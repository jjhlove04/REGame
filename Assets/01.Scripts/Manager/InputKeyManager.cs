using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputKeyManager : MonoBehaviour
{
    private void Awake() {
        
        var thisObj = FindObjectsOfType<InputKeyManager>();
        if(thisObj.Length == 1)
        {
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    } 
    void Update()
    {
        
    }
}
