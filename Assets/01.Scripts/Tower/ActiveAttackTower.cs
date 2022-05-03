using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveAttackTower : Tower
{
    private bool onSkill = false;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Skill();
        }
    }

    protected override void OnButton()
    {
        onSkill = true;
    }

    protected override void Skill()
    {
        onSkill = true;
    }
}
