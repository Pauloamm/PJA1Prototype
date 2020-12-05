using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State : ScriptableObject
{
    protected List<System.Action> onEnterActions;
    protected List<System.Action> onStayActions;
    protected List<System.Action> onExitActions;
    
    [SerializeField]protected List<Transition> stateTransitions;

    
    public delegate void StateChange(State newState);
    public event StateChange StateChanged;


    // Fuck scriptableObjects and their awake method, I hate working with this shit
    public void InitializeState()
    {
        onEnterActions = new List<System.Action>();
        onStayActions = new List<System.Action>();
        onExitActions = new List<System.Action>();
        
        
        CreateActions();
    }
    private void CheckTransitions()
    {
        foreach (Transition transition in stateTransitions)
        {
            if (transition.CanTransition())
            {
                transition.OnTransition(this);
            }
        }
    }

    public void OnEnter()
    {
        
     ExecuteActions(onEnterActions);
     StateChanged?.Invoke(this);

    }


    public void OnStay()
    {
        ExecuteActions(onStayActions);
        CheckTransitions();
    } 

    public void OnExit() => ExecuteActions(onExitActions);

    private void ExecuteActions(List<System.Action> actionsToRun)
    {
        Debug.Log(actionsToRun);
        foreach (System.Action action in actionsToRun)
        {
            action.Invoke();
        }
    }

    
    // Each child must implement its own functions to add to the actions list
    protected abstract void CreateActions();
}