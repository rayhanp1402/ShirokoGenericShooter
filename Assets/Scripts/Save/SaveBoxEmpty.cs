using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using System;
using System.Collections.Generic;

public class SaveBoxEmpty : MonoBehaviour, IPointerClickHandler
{
    public SaveLoadManager saveLoadManager;
    public GameObject saveBoxFilledPrefab;

    private static int currentIndex = 1; // Counter to track the current index

    public void OnPointerClick(PointerEventData eventData)
    {
        // Check if the SaveLoadManager script is assigned
        if (saveLoadManager != null)
        {
            // Call a method in the SaveLoadManager to handle the click event
            saveLoadManager.OnSaveBoxEmptyClicked(this);
        }
    }

    public void PlaceFilledSaveBox(Transform parent)
    {
        // Instantiate the SaveBoxFilled prefab as a child of the specified parent
        GameObject filledSaveBox = Instantiate(saveBoxFilledPrefab, parent);

        // Set the position and rotation of the filled save box to match the empty save box
        filledSaveBox.transform.position = transform.position;
        filledSaveBox.transform.rotation = transform.rotation;

        // Add the SaveBoxFilled script to the instantiated GameObject
        SaveBoxFilled saveBoxFilledScript = filledSaveBox.AddComponent<SaveBoxFilled>();

        // Find the SaveLoadManager component in the scene
        SaveLoadManager saveLoadManager = FindObjectOfType<SaveLoadManager>();

        // Assign the SaveLoadManager component to the SaveBoxFilled script
        if (saveLoadManager != null)
        {
            saveBoxFilledScript.saveLoadManager = saveLoadManager;
        }
        else
        {
            Debug.LogWarning("SaveLoadManager not found in the scene.");
        }

        // Set the save name text based on the current index
        TextMeshProUGUI saveNameText = filledSaveBox.transform.Find("SaveName").GetComponent<TextMeshProUGUI>();
        if (saveNameText != null)
        {
            saveNameText.text = "Save " + currentIndex; // Set the save name based on the current index
            currentIndex++; // Increment the index for the next SaveBoxFilled
        }
        else
        {
            Debug.LogWarning("SaveName TextMeshProUGUI component not found in SaveBoxFilled prefab.");
        }

        // Set the save time text to the current time
        TextMeshProUGUI saveTimeText = filledSaveBox.transform.Find("SaveTime").GetComponent<TextMeshProUGUI>();
        if (saveTimeText != null)
        {
            saveTimeText.text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }
        else
        {
            Debug.LogWarning("SaveTime TextMeshProUGUI component not found in SaveBoxFilled prefab.");
        }

        // Destroy the empty save box
        Destroy(gameObject);
    }
}
