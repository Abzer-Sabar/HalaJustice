using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class finallBoss : MonoBehaviour
{
    [SerializeField]
    private Transform castPoint;
    [HideInInspector]
    public bool playerInSight;

    private states state;
    private enum states
    {
        idle,
        attack,
        teleport,
        death
    }
   



}
