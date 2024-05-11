using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Nightmare;
using UnityEngine.SceneManagement;

public class DialogueManagerEnding : BaseDialogueManager
{
    private TextMeshProUGUI nameText;
    private TextMeshProUGUI dialogueText;
    public List<Dialogue> dialogues;

    private Queue<string> sentences;
    private Image backgroundImage;

    private GameObject currentPrefab;
    private GameObject otherPrefab;

    public Sprite homeSprite;

    public Sprite shirokoSprite;
    public Sprite hoshinoSprite;
    public Sprite serikaSprite;

    private int currentDialogueIndex = 0;
    private bool isDisplayingSentence;

    protected override void Start()
    {
        base.Start();

        // Init sprite dictionary
        characterImages["Shiroko"] = shirokoSprite;
        characterImages["Hoshino"] = hoshinoSprite;
        characterImages["Serika"] = serikaSprite;

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
            "", "Shiroko", "", "Shiroko", "", "Hoshino", 
            "Serika", "Shiroko", "Hoshino", "Serika", "Shiroko", "Hoshino",
            "Shiroko", "Serika", "", "Hoshino", "Shiroko", "",
            "Serika", "Shiroko", "Serika", ""

        };

        GameObject[] prefabs = {
            dialogueNarrationPrefab, dialogueRightPrefab, dialogueNarrationPrefab, dialogueRightPrefab, dialogueNarrationPrefab, dialogueLeftPrefab,
            dialogueLeftPrefab, dialogueRightPrefab, dialogueLeftPrefab, dialogueLeftPrefab, dialogueRightPrefab, dialogueLeftPrefab,
            dialogueRightPrefab, dialogueLeftPrefab, dialogueNarrationPrefab, dialogueLeftPrefab, dialogueRightPrefab, dialogueNarrationPrefab,
            dialogueLeftPrefab, dialogueRightPrefab, dialogueLeftPrefab, dialogueNarrationPrefab, 
        }; 
            
        string[][] sentencesArray = {
            new string[] 
            {
                "The sound of the alarm echoes in the room, a familiar occurence. It feels like we've been here before",
                "Shiroko wakes up and silences her alarm"
            },
            new string[] { "It�s my room, and my bed too. And today is.. Saturday?" },
            new string[] 
            { 
                "Shiroko lets out a sigh of relief, something goes right in this world for once",
                "But she still has to make sure, she has to check on the others"
            },
            new string[] { "Please pick up, someone, anyone" },
            new string[] { 
                "The phone rings for a moment before it's picked up",
                "A few names show up on display, and they don't sound very amused"
            },
            new string[] { "Who is it? Who dares to wake up this princess?" },
            new string[] { "Shiroko? Do you even know what time it is?" },
            new string[] { "That doesn't matter. Quick, tell me what day it is" },
            new string[] { "What? That's it? That's why you wake me up?" },
            new string[] { "Shiroko, I thought you're beyond this kind of thing. Sorry I ever overestimated you" },
            new string[] { "No no, wait, I'm serious this time. It's Saturday right?" },
            new string[] { "Yes, and?" },
            new string[] { "And it's the weekend, right?" },
            new string[] { "Shiroko, she just said it's Saturday" },
            new string[] { "That's all the confirmation she needed, now that it's settled there's nothing else to worry about" },
            new string[] { "Can I go back to sleep now?" },
            new string[] { "Yeah, I don't thi-" },
            new string[] { "And just like that, Hoshino hangs up the phone in a blink" },
            new string[] { "So what's the deal? What kind of nightmare was it?" },
            new string[] 
            { 
                "Oh, you're not gonna believe this one",
                "But it can wait, I wanna go back to sleep"
            },
            new string[] { "I second that, let's talk later" },
            new string[] { 
                "Serika hangs up the call, and silence returns to fill the void left by its noise",
                "Shiroko returns to bed, fixes her pillow, and tugs herself inside the blanket",
                "A feeling of reassurance washes over her as she goes back to sleep",
                "An assurance that no one could ever take the weekends from her",
                "Not this one, or the next, or even the next one, ever"
            }
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
            SceneManager.LoadScene("MainMenu");
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
