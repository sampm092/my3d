using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    // Message displayed if interactable
    public string promptMessage;

    public void baseInteract()
    {
        Interact();
    }

    protected virtual void Interact() { }
}
