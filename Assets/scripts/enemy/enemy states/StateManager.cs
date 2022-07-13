using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    States currentState;


    private void Update()
    {
        runStateMachine();
    }
    private void runStateMachine()
    {
        States nextState = currentState?.runCurrentState();

        if(nextState != null)
        {
            switchState(nextState);
        }
    }

    private void switchState(States nextState)
    {
        currentState = nextState;
    }
}
