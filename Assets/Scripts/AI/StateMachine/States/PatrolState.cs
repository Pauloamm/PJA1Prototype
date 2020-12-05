using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "StateMachine/State/PatrolState")]
public class PatrolState : State
{

    protected override void CreateActions()
    {
        onEnterActions.Add(EnterLookState);
        onStayActions.Add(PatrolAction);
        onExitActions.Add(ExitLookState);
    }

    private void PatrolAction()
    {
        Debug.Log("PATRULHAR A ZONA");
    }
    void EnterLookState()
    {
        Debug.Log("ENTER PATRULHAR A ZONA");

    }

    void ExitLookState()
    {
        Debug.Log(" EXIT PATRULHAR A ZONA");
    }

}
