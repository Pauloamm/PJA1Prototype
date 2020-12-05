using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    private State currentState;
    [SerializeField] private List<State> allStates;

    private void Awake()
    {
        StateInitialization();
        currentState = allStates.ElementAt(0);
    }

    
    void Update()
    {
        currentState.OnStay();
    }


    void StateInitialization()
    {
        foreach (State state in allStates)
        {
            state.InitializeState();
            state.StateChanged += ChangeState;
        }
    }

    
    private void ChangeState(State newState)
    {
        currentState = newState;
    }
}
