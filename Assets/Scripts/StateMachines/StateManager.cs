using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateManager<EState> : MonoBehaviour where EState : Enum
{
    protected Dictionary<EState, BaseState<EState>> States = new Dictionary<EState, BaseState<EState>>();

    protected BaseState<EState> CurrentState;

    protected bool isTransitioningState = false;
    void Start() {
        CurrentState.EnterState();
    }

    public BaseState<EState> GetCurrentState()
    {
        return CurrentState;
    }

    void Update() {

        EState nextStateKey = CurrentState.GetNextState();

        if (!isTransitioningState && nextStateKey.Equals(CurrentState.StateKey))
        {
            CurrentState.UpdateState();
        }
        else if(!isTransitioningState)
        {
            TransitionToNextState(nextStateKey);
        }

        CurrentState.EnterState();
    }

    private void TransitionToNextState(EState nextStateKey)
    {
        isTransitioningState = true;
        CurrentState.ExitState();
        CurrentState = States[nextStateKey];
        CurrentState.EnterState();
        isTransitioningState = false;
    }
}
