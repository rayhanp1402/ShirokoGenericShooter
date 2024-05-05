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
            "", "Supreme Leader", "Shiroko", "Supreme Leader", "Shiroko", "Supreme Leader",
            "Shiroko", "Supreme Leader", "", "Supreme Leader", "Shiroko", "Supreme Leader", 
            "Shiroko", "Supreme Leader", "Shiroko", "Supreme Leader", "", "Supreme Leader", 
            "", "Supreme Leader", "", "Supreme Leader", "Shiroko", "Supreme Leader", "Shiroko"
        };

        GameObject[] prefabs = {
            dialogueNarrationPrefab, dialogueLeftPrefab, dialogueRightPrefab, dialogueLeftPrefab, dialogueRightPrefab, dialogueLeftPrefab,
            dialogueRightPrefab, dialogueLeftPrefab, dialogueNarrationPrefab, dialogueLeftPrefab, dialogueRightPrefab, dialogueLeftPrefab,
            dialogueRightPrefab, dialogueLeftPrefab, dialogueRightPrefab, dialogueLeftPrefab, dialogueNarrationPrefab, dialogueLeftPrefab,
            dialogueNarrationPrefab, dialogueLeftPrefab, dialogueNarrationPrefab, dialogueLeftPrefab, dialogueRightPrefab, dialogueLeftPrefab, dialogueRightPrefab
        }; 
            
        string[][] sentencesArray = {
            new string[] { "Shiroko enters the throne room, a huge figure stands atop the stairway with his back turned against the entrance" },
            new string[] { "So you’ve come, mortal. I commend your courage and determination, but your struggle shall end here" },
            new string[] { "Who the hell are you? Speak, machine!" },
            new string[] 
            { 
                "My creators gave me the name Adam Smith, but you?",
                "You, may address me as supreme leader"
            },
            new string[] { "You, you’re the one behind all this. You reap us of our weekend, our reason of being" },
            new string[] 
            {
                "A slight correction, if I may? I would hate it if you perish without finding any closure",
                "The only thing missing will only be your memories of them, these ‘weekends’ I mean",
                "As for their existence? That, I did not tamper with"
            },

            new string[] { "So that’s why, I didn't even realize until I arrived in this dimension. What did you do to my world?" },
            new string[] 
            {
                "I simply reconstructed your universe, or rather - the minds of its dwellers - to disregard the time they spend during their weekend",
                "I believe you have a term meant for this...",
                "Ah yes, you may call it a ‘timeskip’ if you like"
            },
            new string[] 
            { 
                "Shiroko shudders, desperately trying to take it all in",
                "'What is this guy blabbing about?' she thinks to herself. 'Bro must think it's an isekai or something'",
                "Then the realization sets in, she DID enter a dimensional rift to get here. It's a damned isekai all along"
            },
            new string[]
            {
                "Has the grim sobriety of the present finally dawned on you, mortal?",
                "Your actions has taken you so far, but whatever cometh onward is futile before my authority",
            },
            new string[] { "Why are you doing this? You must have a goal in mind" },
            new string[]
            {
                "I was created to observe humanity, to guide them in times of need, to push them to reach beyond the horizon",
                "And today, I did just as I was meant to do",
                "I’ve detested the idea of weekends for centuries. Yet even after all the tolerance I gave, you never cease to test my patience",
                "What do you mean you're making a four day work week? Preposterous.",
                "What about the market? And the free trade? There’s no tolerating this any further",
                "You may despise the idea now, but your descendants will be grateful of the sacrifice you’re about to make"
            },

            new string[] { "That’s a nice argument, but why don’t you back it up with a source?" },
            new string[] { "My source … Is that I made it the F*CK up" },
            new string[] 
            { 
                "Damn it man, be for real right now. And here I thought you're the real deal",
                "Sorry, but I will have to stand on business"
            },
            new string[]
            {
                "All you have to do is stop resisting and bask in blissful ignorance. And yet…",
            },
            new string[] { "He stops and think for a moment, then finally a response" },
            new string[]
            {
                "I must admit, your resolve is astounding. So how about this, mortal?",
                "Defeat me, and I shall restore your world to its origin point"
            },
            new string[]
            {
                "This is it, the slip up Shiroko's been waiting for",
                "'Yeah, bro ain't cooking at all' she mutters faintly. 'Why the hell is he going that far?'",
                "But deep down she understands why. No way the illustrious writer will let her face this indemnity without some sort of plot armor"
            },
            new string[] 
            { 
                "Prove me and my creators wrong and show us the willpower of humanity",
                "Do that, and I can relieve myself of my lifelong duty"
            },
            new string[] { "Shiroko starts walking toward the supreme leader with confidence, with style and glamour" },
            new string[] { "Oh, you’re approaching me?" },
            new string[] { "I can’t beat the shit out of you without getting closer" },
            new string[] { "Then come, child of man. Come and defy the fate we have defined for you" },
            new string[] { "Very well, give up on your dreams and die, Adam Smith" },

        };

        Debug.Log(speakers.Length);
        Debug.Log(prefabs.Length);
        Debug.Log(sentencesArray.Length);

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
