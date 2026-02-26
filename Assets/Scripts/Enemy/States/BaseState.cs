// The required structure for every AI state in enemy’s state machine
public abstract class BaseState
{
    public Enemy enemy; // Access enemy components
    public StateMachine stateMachine; // Change to another state
    public abstract void Enter(); // Runs once when state starts.
    public abstract void Perform(); // Runs every frame while state is active.
    public abstract void Exit(); // Runs once when leaving state.
}
