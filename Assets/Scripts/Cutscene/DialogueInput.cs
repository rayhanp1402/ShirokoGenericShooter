using UnityEngine;
using System.Collections;
using TMPro;

public class DialogueInput : MonoBehaviour
{
    // Reference to the dialogue manager
    public DialogueManager dialogueManager;

    void Update()
    {
        // Check for mouse click or space bar press
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
        {
            // Trigger the DisplayNextSentence method in the dialogue manager
            dialogueManager.DisplayNextSentence();
        }
    }
}
