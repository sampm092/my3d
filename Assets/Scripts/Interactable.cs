using UnityEngine;

public abstract class Interactable : MonoBehaviour
// abstract = cannot be attached to game object, means to be inherited, and extended with other script
{
    // Message displayed if interactable
    public string promptMessage;

    // If true → use Unity Events system
    // If false → only use code override
    public bool useEvents;

    public void baseInteract()
    {
        //jika ada assigned function dari editor, maka lakukan itu
        if (useEvents)
        {
            GetComponent<InteractionEvent>().OnInteract.Invoke(); // perform a behaviour based on assigned function
        }
        // jika tidak ada maka gunakan function yang ada pada extended script yang di-attach pada interactable object
        Interact(); //inherited function from extended script
    }

    protected virtual void Interact() { } //use virtual void because can be overridden
}
