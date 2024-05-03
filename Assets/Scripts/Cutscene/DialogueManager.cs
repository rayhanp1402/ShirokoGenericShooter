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

    // Store a reference to the current prefab type
    private string currentPrefabName;

    void Start()
    {
        sentences = new Queue<string>();

        // Set the initial prefab type
        currentPrefabName = "DialogueLeft";

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

                        // Store the current prefab name
                        currentPrefabName = prefabName;

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

        // Load the next prefab based on the current prefab type
        string nextPrefabName = currentPrefabName == "DialogueLeft" ? "DialogueRight" : "DialogueLeft";
        string nextPrefabPath = nextPrefabName == "DialogueLeft" ? dialogueLeftPrefabPath : dialogueRightPrefabPath;

        // Find the current GameObject
        GameObject currentPrefab = GameObject.Find(currentPrefabName);
        if (currentPrefab != null)
        {
            // Get the position and parent of the current prefab
            Transform parent = currentPrefab.transform.parent;
            Vector3 position = currentPrefab.transform.position;

            // Load the next prefab dynamically
            GameObject nextPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(nextPrefabPath);
            if (nextPrefab != null)
            {
                // Instantiate the next prefab at the same position and with the same parent
                GameObject nextGameObject = Instantiate(nextPrefab, position, Quaternion.identity, parent);

                // Destroy the current GameObject
                Destroy(currentPrefab);
            }
            else
            {
                Debug.LogError("Failed to load the next prefab from path: " + nextPrefabPath);
            }
        }
        else
        {
            Debug.LogError("Failed to find the current prefab: " + currentPrefabName);
        }
    }
}
