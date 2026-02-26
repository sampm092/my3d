using UnityEngine;

public class PatrolState : BaseState
{
    public int waypointIndex; // track which waypoint currently targeting
    public float waitTime; // enemy wait time at one waypoint

    // Start is called before the first frame update
    public override void Enter() { }

    public override void Perform()
    {
        PatrolCycle();
    }

    public override void Exit() { }

    public void PatrolCycle()
    {
        if (enemy.Agent.remainingDistance < 0.2f) // check if enemy reach waypoint
        {
            waitTime += Time.deltaTime;
            if (waitTime > 3)
            {
                if (waypointIndex < enemy.path.waypoints.Count - 1) // If not at last waypoint
                    waypointIndex++; // increase index point
                else // if at last waypoint
                    waypointIndex = 0; // reset index to start point
                enemy.Agent.SetDestination(enemy.path.waypoints[waypointIndex].position); // move enemy based on the index point
                waitTime = 0;
            }
        }
    }
}
