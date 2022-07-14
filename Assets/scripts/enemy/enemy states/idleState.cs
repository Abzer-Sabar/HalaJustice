using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class idleState : States
{
    public AttackState attackState;
    

    public bool playerInSight;
    public override States runCurrentState()
    {
        if (playerInSight)
        {
            Debug.Log("player is in sight");
            return attackState;
        }
        else
        {
            return this;
        }
    }

}
