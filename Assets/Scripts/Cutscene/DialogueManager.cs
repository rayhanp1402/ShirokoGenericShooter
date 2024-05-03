using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    // Define the public variable to hold the path to the DialogueRight prefab
    public string dialogueRightPrefabPath;
    public string dialogueLeftPrefabPath;

    private TextMeshProUGUI nameText;
    private TextMeshProUGUI dialogueText;
    private Queue<string> sentences;

    void Start()
    {
        sentences = new Queue<string>();

        // Create a new dialogue instance with specific data
        Dialogue dialogue = new Dialogue();
        dialogue.name = "Shiroko-chan";
        dialogue.sentences = new string[]
        {
            "What's up fellow kids? It's a great day isn' it?",
            "Today we're about to do what's called a pro gamer move",
            "Let's cut to the chase so we can immediately own these kids"
        };

        // Start the dialogue
        StartDialogue(dialogue);
    }

    public void StartDialogue(Dialogue dialogue)
    {
        // Define the names of the prefabs
        string[] prefabNames = { "DialogueLeft", "DialogueRight", "DialogueNarration" };

        foreach (string prefabName in prefabNames)
        {
            // Find the prefab GameObject by name
            GameObject prefab = GameObject.Find(prefabName);

            if (prefab != null)
            {
                // Search for the nameText and dialogueText components within the prefab hierarchy
                Transform characterNameTransform = prefab.transform.Find("CharacterName");
                Transform dialogueBoxTransform = prefab.transform.Find("DialogueBox");

                if (characterNameTransform != null && dialogueBoxTransform != null)
                {
                    nameText = characterNameTransform.Find("CharacterText").GetComponent<TextMeshProUGUI>();
                    dialogueText = dialogueBoxTransform.Find("DialogueText").GetComponent<TextMeshProUGUI>();

                    if (nameText != null && dialogueText != null)
                    {
                        // Set the name text
                        nameText.text = dialogue.name;

                        // Clear previous sentences and enqueue new sentences
                        sentences.Clear();
                        foreach (string sentence in dialogue.sentences)
                        {
                            sentences.Enqueue(sentence);
                        }

                        // Display the next sentence
                        DisplayNextSentence();

                        // Break out of the loop once found
                        break;
                    }
                }
            }
        }

        // If nameText or dialogueText is still null, log an error
        if (nameText == null || dialogueText == null)
        {
            Debug.LogError("NameText or DialogueText not found in any of the prefabs.");
        }
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    public void EndDialogue()
    {
        Debug.Log("End of conversation");

        // Find DialogueLeft GameObject
        GameObject dialogueLeft = GameObject.Find("DialogueLeft");
        if (dialogueLeft != null)
        {
            Debug.Log("PPPPPp");
            // Get the position and parent of DialogueLeft
            Transform parent = dialogueLeft.transform.parent;
            Vector3 position = dialogueLeft.transform.position;

            // Load DialogueRight prefab dynamically
            GameObject dialogueRightPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(dialogueRightPrefabPath);
            if (dialogueRightPrefab != null)
            {
                // Instantiate DialogueRight prefab at the same position and with the same parent
                GameObject dialogueRight = Instantiate(dialogueRightPrefab, position, Quaternion.identity, parent);

                // Destroy DialogueLeft GameObject
                Destroy(dialogueLeft);
            }
            else
            {
                Debug.LogError("Failed to load DialogueRight prefab from path: " + dialogueRightPrefabPath);
            }
        }
    }
}
