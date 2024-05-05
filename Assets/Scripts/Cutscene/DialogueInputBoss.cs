using UnityEngine;
using System.Collections;
using TMPro;

public class DialogueInputBoss : BaseDialogueInput<DialogueManagerBoss>
{

    protected override void HandleInput()
    {
        // Check if the dialogue manager is displaying a sentence
        bool isDisplaying = dialogueManager.getDisplayingFlag();
        if (!isDisplaying)
        {
            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
            {
                dialogueManager.DisplayNextSentence();
            }
        }
    }
}
