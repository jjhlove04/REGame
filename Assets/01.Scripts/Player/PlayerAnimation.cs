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
    
    public void SetRun(bool value)          
    {
        upperanimator.SetBool("Run", value);
        loweranimator.SetBool("Run", value);
    }   
    
    public void IsForward()
    {
        upperanimator.SetFloat("Dir", 1);
        loweranimator.SetFloat("Dir", 1);
    }

    public void IsBack()
    {
        upperanimator.SetFloat("Dir", -1);
        loweranimator.SetFloat("Dir", -1);
    }

    public void SetSide(bool value)
    {
        upperanimator.SetBool("Move", value);
        loweranimator.SetBool("Side", value);
        loweranimator.SetBool("Move", !value);
    }

    public void SetMove(bool value)
    {
        upperanimator.SetBool("Side", !value);
        loweranimator.SetBool("Side", !value);
        upperanimator.SetBool("Move", value);
        loweranimator.SetBool("Move", value);
    }

    public void Idle()
    {
        loweranimator.SetBool("Side", false);
        upperanimator.SetBool("Move", false);
        loweranimator.SetBool("Move", false);
    }

    public void IsRight()
    {
        upperanimator.SetBool("Left", false);
        loweranimator.SetBool("Left", false);
    }

    public void Isleft()
    {
        upperanimator.SetBool("Left", true);
        loweranimator.SetBool("Left", true);
    }
}
