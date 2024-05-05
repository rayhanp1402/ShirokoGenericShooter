using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueManagerBoss : BaseDialogueManager
{
    private TextMeshProUGUI nameText;
    private TextMeshProUGUI dialogueText;
    public List<Dialogue> dialogues;

    private Queue<string> sentences;
    private Image backgroundImage;

    private GameObject currentPrefab;
    private GameObject otherPrefab;

    public Sprite throneSprite;

    public Sprite shirokoSprite;
    public Sprite bossSprite;

    private int currentDialogueIndex = 0;
    private bool isDisplayingSentence;

    protected override void Start()
    {
        base.Start();

        // Init sprite dictionary
        characterImages["Shiroko"] = shirokoSprite;
        characterImages["Supreme Leader"] = bossSprite;

        // Init sentences and dialogues
        sentences = new Queue<string>();
        isDisplayingSentence = false;

        LoadDialogues();

        Canvas canvas = FindObjectOfType<Canvas>();

        if (canvas != null)
        {
            backgroundImage = canvas.GetComponentInChildren<Image>();
        }

        StartNextDialogue();
    }

    protected override void LoadDialogues()
    {
        dialogues = new List<Dialogue>();

        // Define the dialogues
        string[] speakers = { 
            "Supreme Leader"
        };

        GameObject[] prefabs = {
            dialogueRightPrefab, dialogueLeftPrefab, dialogueNarrationPrefab
        }; 
            
        string[][] sentencesArray = {
            new string[] { "Ohayou minna san" },

        };

        // Add each dialogue into the dialogues list
        for (int i = 0; i < speakers.Length; i++)
        {
            Dialogue dialogue = new Dialogue(speakers[i], prefabs[i], sentencesArray[i]);
            dialogues.Add(dialogue);
        }

    }

    protected override void StartNextDialogue()
    {
        if (currentDialogueIndex < dialogues.Count)
        {
            Dialogue currentDialogue = dialogues[currentDialogueIndex];

            currentPrefab = currentDialogue.prefab;

            // Set the background sprite based on the current dialogue index
            /*
            if (currentDialogueIndex == 11)
            {
                backgroundImage.sprite = streetSprite;
            } 
            else if (currentDialogueIndex == 12)
            {
                backgroundImage.sprite = schoolSprite;
            }
            */
          
            // Hide all other prefabs
            dialogueLeftPrefab.SetActive(false);
            dialogueRightPrefab.SetActive(false);
            dialogueNarrationPrefab.SetActive(false);

            // Activate the current prefab
            currentPrefab.SetActive(true);

            // Start the dialogue
            StartDialogue(currentDialogue);

            // Set the character image based on the character's name
            if (characterImages.ContainsKey(currentDialogue.name))
            {
                Image image = currentPrefab.GetComponentInChildren<Image>();
                if (image != null)
                {
                    image.sprite = characterImages[currentDialogue.name];
                }
            }
        }
        else
        {
            Debug.Log("No more dialogues to start.");
        }
    }



    protected override void StartDialogue(Dialogue dialogue)
    {
        // Find the nameText and dialogueText in the current prefab hierarchy
        nameText = currentPrefab.transform.Find("CharacterName")?.Find("CharacterText")?.GetComponent<TextMeshProUGUI>();
        dialogueText = currentPrefab.transform.Find("DialogueBox").Find("DialogueText").GetComponent<TextMeshProUGUI>();

        // Set the character name text if it exists
        if (nameText != null)
        {
            nameText.text = dialogue.name;
        }

        // Clear previous sentences and enqueue new sentences
        sentences.Clear();
        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        // Display the next sentence
        DisplayNextSentence();
    }

    public override bool getDisplayingFlag()
    {
        return isDisplayingSentence;
    }

    public override void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();

        // Check if the coroutine is already running
        if (!isDisplayingSentence)
        {
            StartCoroutine(TypeSentence(sentence));
        }
    }

    IEnumerator TypeSentence(string sentence)
    {
        isDisplayingSentence = true;

        dialogueText.text = "";

        // Display each letter of the sentence gradually
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }

        isDisplayingSentence = false;
    }


    protected override void EndDialogue()
    {
        currentDialogueIndex++;

        StartNextDialogue();
    }
}
