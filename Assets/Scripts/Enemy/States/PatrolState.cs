using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : BaseState
{
    public int waypointIndex; // track which waypoint currently targeting
    public float waitTime;

    // Start is called before the first frame update
    public override void Enter() { }

    public override void Perform()
    {
        PatrolCycle();
    }

    public override void Exit() { }

    public void PatrolCycle()
    {
        if (enemy.Agent.remainingDistance < 0.2f)
        {
            waitTime += Time.deltaTime;
            if (waitTime > 3)
            {
                if (waypointIndex < enemy.path.waypoints.Count - 1)
                    waypointIndex++;
                else
                    waypointIndex = 0;
                enemy.Agent.SetDestination(enemy.path.waypoints[waypointIndex].position);
                waitTime = 0;
            }
        }
    }
}
