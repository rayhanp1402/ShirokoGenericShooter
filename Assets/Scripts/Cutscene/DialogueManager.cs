using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public GameObject dialogueLeftPrefab;
    public GameObject dialogueRightPrefab;
    public GameObject dialogueNarrationPrefab;

    private TextMeshProUGUI nameText;
    private TextMeshProUGUI dialogueText;
    public List<Dialogue> dialogues;

    private Queue<string> sentences;
    private Image backgroundImage;

    private GameObject currentPrefab;
    private GameObject otherPrefab;

    public Sprite homeSprite;
    public Sprite streetSprite;
    public Sprite schoolSprite;

    public Sprite shirokoSprite;
    public Sprite hoshinoSprite;
    public Sprite serikaSprite;

    // Dictionary to map character names to their respective images
    public Dictionary<string, Sprite> characterImages;

    private int currentDialogueIndex = 0;
    private bool isDisplayingSentence;

    void Start()
    {
        sentences = new Queue<string>();
        isDisplayingSentence = false;

        characterImages = new Dictionary<string, Sprite>();
        characterImages["Shiroko"] = shirokoSprite;
        characterImages["Hoshino"] = hoshinoSprite;
        characterImages["Serika"] = serikaSprite;

        LoadDialogues();

        Canvas canvas = FindObjectOfType<Canvas>();

        if (canvas != null)
        {
            backgroundImage = canvas.GetComponentInChildren<Image>();
        }

        StartNextDialogue();
    }

    void LoadDialogues()
    {
        dialogues = new List<Dialogue>();

        // Define the dialogues
        string[] speakers = { 
            "", "Shiroko", "", "Shiroko", "Serika", "Shiroko", 
            "Serika", "", "Shiroko", "Serika", "", "", "",
            "Shiroko", "Hoshino", "Serika", "Shiroko", "Hoshino", "Serika", 
            "Shiroko", "", "Shiroko", "", "Serika", "Hoshino", 
            "", "Hoshino", "Shiroko", "", "Shiroko", "Serika",
            "Hoshino", "Serika", "Hoshino", "", "Shiroko", "Hoshino", 
            "Shiroko", "Serika", "Shiroko", ""
        };

        GameObject[] prefabs = {
            dialogueNarrationPrefab, dialogueRightPrefab, dialogueNarrationPrefab, dialogueRightPrefab, dialogueLeftPrefab, dialogueRightPrefab, dialogueLeftPrefab, 
            dialogueNarrationPrefab, dialogueRightPrefab, dialogueLeftPrefab, dialogueNarrationPrefab, dialogueNarrationPrefab, dialogueNarrationPrefab,
            dialogueRightPrefab, dialogueLeftPrefab, dialogueLeftPrefab, dialogueRightPrefab, dialogueLeftPrefab, dialogueLeftPrefab,
            dialogueRightPrefab, dialogueNarrationPrefab, dialogueRightPrefab, dialogueNarrationPrefab, dialogueLeftPrefab, dialogueLeftPrefab, 
            dialogueNarrationPrefab, dialogueLeftPrefab, dialogueRightPrefab, dialogueNarrationPrefab, dialogueRightPrefab, dialogueLeftPrefab, 
            dialogueLeftPrefab, dialogueLeftPrefab, dialogueLeftPrefab, dialogueNarrationPrefab, dialogueRightPrefab, dialogueLeftPrefab, 
            dialogueRightPrefab, dialogueLeftPrefab, dialogueRightPrefab, dialogueNarrationPrefab}; 
            
        string[][] sentencesArray = {
            new string[] { "The sound of the alarm echoes in the room, until it’s silenced by Shiroko’s hand chop" },
            new string[]
            {
                "Oh beautiful saturday, a day meant to be spent sleeping until noon",
                "Alas, I have awakened from my slumber because of this... devilish abomination",
                "Oh well, time to finish that dream I just ha-"
            },
            new string[]
            {
                "Her sentence remains incomplete, interrupted by the ring of a phone call",
                "The caller’s name displayed vividly on the screen, it’s her friend Serika on the other end",
                "'This better be important, or there will be hell to pay' she says to herself",
            },
            new string[] { "Hello, sorry but you got the wrong number" },
            new string[]
            {
                "Oh thank God you’re picking up, get out of bed and get dressed already",
                "Unless of course, you’re planning on being late"
            },
            new string[] { "Late? Late for what? We have no events for Saturday" },
            new string[]
            {
                "Oh my dear Shiroko, would you do me a favor?",
                "Please look at your phone’s calendar and read it aloud for me"
            },
            new string[] 
            { 
                "Shiroko proceeds to do just that, and the upcoming horror has never appeared even in her wildest dreams", 
                "She jumped out of bed with horror as she shouts" 
            },
            new string[] { "It’s ... Monday? But… but… I thought…" },
            new string[] { "Right, I’ll see you at school then" },
            new string[] { "With the blessing of Hermes, Shiroko gets dressed and proceeds to run to school with the pace and speed of a track star" },
            new string[]
            {
                "The school bell is ringing, the sun is rising, and beneath its warm light Shiroko is running with all her might",
                "As she goes, something tugs at her mind, the scenery feels different. Is it the trees? The clouds floating above her head?",
                "Or perhaps … the dimensional rifts staring into her along the way?",
                "She has a lot of questions and someone better have the answers"
            },
            new string[]
            {
                "Shiroko reaches the school gate and finds a few familiar faces not far from where she stops",
                "It’s Serika and Hoshino, just about to enter the school hallway"
            },
            
            new string[] { "Morning girls, looks like I made it just in time huh?" },
            new string[] { "Morning Shiroko, it’s good to see you" },
            new string[] { "Morning, good thing I called you earlier, yes? What would you have done without me?" },
            new string[] { "Alright, guess I owe you one this time. Why aren't you two in class already?" },
            new string[] { "I felt like eating fancy today so I cooked a variety of things, took a while but it was well worth it" },
            new string[] { "As for me, no particular reason. Just wanna go a bit later than usual" },

            new string[] { "I see…" },
            new string[] { "Suddenly a strong urge comes up in her mind. There’s something she wants to, no, needs to ask. And she has to do it now" },
            new string[] { "Have you two felt like something’s off lately? I mean, what’s the deal with that thing?" },
            new string[] { "She gulps and points to a rift on the school grounds" },  
            new string[] { "Ah that? It’s been around forever though" },
            new string[] { "Yeah, it’s the one they use for the-" },

            new string[] { "Her words stop abruptly for an unexplainable reason, she paused for a while before continuing" },
            new string[] { "Ah, it’s probably nothing important anyway. Why do you care?" },
            new string[] { "Right, why should I?" },
            new string[] { "She says that to herself, but something still tugs at her mind. There’s something else she needs to ask" },
            new string[] { "You know, it’s funny but I don’t remember what I was doing during the weekend. What about you? What were you up to?" },
            new string[] { "Hmm, what was I doing again? Watching anime maybe? What about you Hoshino?" },

            new string[] { "I think I was…. Uhh… I don’t really remember" },
            new string[] { "Oh well, it will come back to us. Let’s just go inside, we will be late for class" },
            new string[] { "Agreed, my perfect attendance is at stake here. Come on, Shiroko!" },
            new string[] 
            {
                "Shiroko wishes to agree, but her legs won’t step forward",
                "There’s this feeling again. A sense of urgency, telling her not to go",
                "Her mind shifts to the rift on her left, there’s something beyond it that keeps on pulling her attention",
                "Then, she decided. She has to look for answers now, or the opportunity will vanish forever"
            },
            new string[] { "I’m sorry girls, but I’m not coming with you" },
            new string[] { "Eh, but why?" },

            new string[] { "There’s something I need to find out, and my guts tell me I have to do it myself" },
            new string[] { "You sure? We can tag along if you want" },
            new string[] { "Don’t worry, I got this one. I’ll see you all again soon" },
            new string[]
            {
                "Shiroko inhales and exhales her breath slowly",
                "'here goes nothing' she mumbles. She grabs her trusted white fang and enters the rift",
                "As she enters the dimension, no fear has her shackled because of a single conviction",
                "A daring endeavor, but at that moment her conviction far outweighs the fear that's trying to shackle her",
                "A conviction that comes what may on her journey, she and she alone shall prevail"
            },

        };

        // Add each dialogue into the dialogues list
        for (int i = 0; i < speakers.Length; i++)
        {
            Dialogue dialogue = new Dialogue(speakers[i], prefabs[i], sentencesArray[i]);
            dialogues.Add(dialogue);
        }

    }

    void StartNextDialogue()
    {
        if (currentDialogueIndex < dialogues.Count)
        {
            Dialogue currentDialogue = dialogues[currentDialogueIndex];

            currentPrefab = currentDialogue.prefab;

            // Set the background sprite based on the current dialogue index
            if (currentDialogueIndex == 11)
            {
                backgroundImage.sprite = streetSprite;
            } 
            else if (currentDialogueIndex == 12)
            {
                backgroundImage.sprite = schoolSprite;
            }
          
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

                    // Resize the image if the character is Serika
                    if (currentDialogue.name == "Serika")
                    {
                        RectTransform rectTransform = image.GetComponent<RectTransform>();
                        if (rectTransform != null)
                        {
                            rectTransform.sizeDelta = new Vector2(608, 580);
                        }
                    }
                }
            }
        }
        else
        {
            Debug.Log("No more dialogues to start.");
        }
    }



    public void StartDialogue(Dialogue dialogue)
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

    public void DisplayNextSentence()
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


    public void EndDialogue()
    {
        currentDialogueIndex++;

        StartNextDialogue();
    }
}
