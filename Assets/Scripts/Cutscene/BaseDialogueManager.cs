using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;

public abstract class BaseDialogueManager : MonoBehaviour
{
    // References to the prefabs
    public GameObject dialogueLeftPrefab;
    public GameObject dialogueRightPrefab;
    public GameObject dialogueNarrationPrefab;

    // Dictionary to map character names to their respective images
    protected Dictionary<string, Sprite> characterImages;

    protected virtual void Start()
    {
        characterImages = new Dictionary<string, Sprite>();
    }

    // Abstract methods to be implemented by child classes
    protected abstract void LoadDialogues();
    protected abstract void StartNextDialogue();
    protected abstract void StartDialogue(Dialogue dialogue);
    public abstract void DisplayNextSentence();
    protected abstract void EndDialogue();
}
