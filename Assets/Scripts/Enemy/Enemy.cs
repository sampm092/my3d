using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private StateMachine statemachine;
    private NavMeshAgent agent;
    public NavMeshAgent Agent
    {
        get => agent; // When someone asks for Agent, return agent.
    } // encapsulation to avoid changing its value

    [SerializeField]
    private string currentState;
    public Path path;
    private GameObject player;
    public float sightDistance = 20f;
    public float fieldOfView = 85f;

    // Start is called before the first frame update
    void Start()
    {
        statemachine = GetComponent<StateMachine>();
        agent = GetComponent<NavMeshAgent>();
        statemachine.Initialize();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update() { }

    public bool CanSeePlayer()
    {
        if (player != null)
        {
            if (Vector3.Distance(transform.position, player.transform.position) < sightDistance)
            {
                Vector3 targetDirection = player.transform.position - transform.position;
                float angleToPlayer = Vector3.Angle(targetDirection, transform.forward);
                if (angleToPlayer >= -fieldOfView && angleToPlayer <= fieldOfView)
                {
                    Ray ray = new Ray(transform.position, targetDirection);
                }
            }
        }
        return true; //wrong code
    }
}
