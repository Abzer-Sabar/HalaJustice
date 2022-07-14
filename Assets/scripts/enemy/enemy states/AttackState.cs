using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : States
{
    public bool playerInRange = false;
    public override States runCurrentState()
    {
        Debug.Log("i have attacked the player");
        return this;
    }
}
