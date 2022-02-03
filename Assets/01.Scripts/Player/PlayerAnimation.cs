using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField]
    public Animator upperanimator;
    [SerializeField]
    public Animator loweranimator;
    // Start is called before the first frame update

    // Update is called once per frame
    
    public void SetAttack(bool value)
    {
        upperanimator.SetBool("Attack",value);
        loweranimator.SetBool("Attack",value);
    }   
    
    public void SetMove(bool value)
    {
        upperanimator.SetBool("Move", value);
        loweranimator.SetBool("Move", value);
    }    
    
    public void SetRun(bool value)          
    {
        upperanimator.SetBool("Run", value);
        loweranimator.SetBool("Run", value);
    }   
    
    public void SetDiretion(float value)
    {
        if(value > 0)
        {
            upperanimator.SetFloat("Dir", 1);
            loweranimator.SetFloat("Dir", 1);
        }        
        
        else if(value < 0)
        {
            upperanimator.SetFloat("Dir", -1);
            loweranimator.SetFloat("Dir", -1);
        }

    }

    public void SetSide(bool value)
    {
        upperanimator.SetBool("Side", value);
        loweranimator.SetBool("Side", value);
    }

    public void IsRight(float value)
    {
        upperanimator.SetBool("Left", value < 0);
        loweranimator.SetBool("Left", value < 0);
    }
}
