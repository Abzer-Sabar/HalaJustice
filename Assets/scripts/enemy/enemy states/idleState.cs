using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class idleState : States
{
    public AttackState attackState;

    private bool playerInSight;
    public override States runCurrentState()
    {
        if (playerInSight)
        {
            return attackState;
        }
        else
        {
            return this;
        }
    }
}
