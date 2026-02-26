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

    // Start is called before the first frame update
    void Start()
    {
        statemachine = GetComponent<StateMachine>();
        agent = GetComponent<NavMeshAgent>();
        statemachine.Initialize();
    }

    // Update is called once per frame
    void Update() { }
}
