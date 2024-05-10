using UnityEngine;

public abstract class BaseDialogueInput<T> : MonoBehaviour where T : BaseDialogueManager
{
    // Reference to the dialogue manager of type T
    public T dialogueManager;

    protected abstract void HandleInput();

    protected virtual void Update()
    {
        HandleInput();
    }
}
