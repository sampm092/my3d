using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public BaseState activeState;
    public PatrolState patrolState;

    // Start is called before the first frame update
    void Start() { }

    public void Initialize()
    {
        // setup default state
        patrolState = new PatrolState();
        SwitchState(patrolState);
    }

    // Update is called once per frame
    void Update()
    {
        if (activeState != null)
        {
            activeState.Perform();
        }
    }

    public void SwitchState(BaseState newState)
    {
        //check if activeState is there, and if yes then exit state to avoid duplication
        activeState?.Exit();
        //switch to another state
        activeState = newState;

        if (activeState != null) //make sure new state not null
        {
            // set new state
            activeState.stateMachine = this;
            activeState.enemy = GetComponent<Enemy>(); //assign state enemy class
            activeState.Enter();
        }
    }
}
