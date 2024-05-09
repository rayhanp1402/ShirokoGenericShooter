using UnityEngine;
using System;
using TMPro;

public class SaveLoadManager : MonoBehaviour
{
    public GameObject saveBoxEmptyPrefab;
    public GameObject saveBoxFilledPrefab;
    public Transform saveMenuCanvas;

    private static int currentIndex = 1;

    // Save player data to PlayerPrefs
    public void SavePlayerData()
    {
        Debug.Log("Saving player data...");
    }

    // Load player data from PlayerPrefs
    public void LoadPlayerData()
    {
        Debug.Log("Loading player data...");
    }

    public void OnSaveBoxEmptyClicked(SaveBoxEmpty saveBoxEmpty)
    {
        PlaceFilledSaveBox(saveBoxEmpty);
        SavePlayerData();
    }

    public void OnSaveBoxFilledClicked()
    {
        Debug.Log("Save box filled clicked!");
        LoadPlayerData();
    }

    private void PlaceFilledSaveBox(SaveBoxEmpty saveBoxEmpty)
    {
        // Instantiate the SaveBoxFilled prefab as a child of the SaveMenuCanvas
        GameObject filledSaveBox = Instantiate(saveBoxFilledPrefab, saveMenuCanvas);

        // Set the position and rotation of the filled save box to match the empty save box
        filledSaveBox.transform.position = saveBoxEmpty.transform.position;
        filledSaveBox.transform.rotation = saveBoxEmpty.transform.rotation;

        SaveBoxFilled saveBoxFilledScript = filledSaveBox.AddComponent<SaveBoxFilled>();
        saveBoxFilledScript.saveLoadManager = this;

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
        Destroy(saveBoxEmpty.gameObject);
    }

    public void PlaceEmptySaveBox(SaveBoxFilled saveBoxFilled, SaveLoadManager saveLoadManager)
    {
        // Instantiate an empty save box in the same position as the filled save box
        GameObject emptySaveBox = Instantiate(saveBoxEmptyPrefab, saveBoxFilled.transform.position, saveBoxFilled.transform.rotation, saveMenuCanvas);

        SaveBoxEmpty emptyBoxScript = emptySaveBox.AddComponent<SaveBoxEmpty>();

        if (emptyBoxScript != null)
        {
            emptyBoxScript.saveLoadManager = saveLoadManager;
            emptyBoxScript.saveBoxFilledPrefab = saveBoxFilledPrefab;
        }
        // Destroy the filled save box
        Destroy(saveBoxFilled.gameObject);
    }
}
