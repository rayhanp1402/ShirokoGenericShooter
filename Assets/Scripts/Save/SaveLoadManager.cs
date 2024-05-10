using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;

public class SaveLoadManager : MonoBehaviour
{
    public GameObject saveBoxEmptyPrefab;
    public GameObject saveBoxFilledPrefab;
    public Transform saveMenuCanvas;
    public Transform loadMenuCanvas;
    public Transform hudCanvas;

    private static int currentIndex = 1;

    // Save player data to PlayerPrefs
    public void SavePlayerData()
    {
        // Sample player data
        // PlayerPrefs.SetFloat("PlayerHealth", 100f);
        // PlayerPrefs.SetInt("PlayerLevel", 1);
        // PlayerPrefs.SetString("PlayerPosition", "x:0, y:0, z:0");
        FindScoreAndCoinText();

        Debug.Log("Player data saved.");
    }

    // Load player data from PlayerPrefs
    public void LoadPlayerData()
    {
        // Retrieve player data from PlayerPrefs
        float health = PlayerPrefs.GetFloat("PlayerHealth");
        int level = PlayerPrefs.GetInt("PlayerLevel");
        string position = PlayerPrefs.GetString("PlayerPosition");

        Debug.Log("Player data loaded:");
        Debug.Log("Health: " + health);
        Debug.Log("Level: " + level);
        Debug.Log("Position: " + position);
    }

    // Overwrite the current save data with the new save data
    public void OverwriteSaveData(SaveBoxFilled saveBoxFilled)
    {
        // Update the save time text to the current time
        TextMeshProUGUI saveTimeText = saveBoxFilled.transform.Find("SaveTime").GetComponent<TextMeshProUGUI>();
        if (saveTimeText != null)
        {
            saveTimeText.text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            // Sample overwrite data
            PlayerPrefs.SetFloat("PlayerHealth", 80f);

            Debug.Log("Save data overwritten.");
        }
        else
        {
            Debug.LogWarning("SaveTime TextMeshProUGUI component not found in SaveBoxFilled prefab.");
        }
    }

    // Delete the current save data
    public void DeleteSaveData()
    {
        PlayerPrefs.DeleteAll();

        Debug.Log("Save data deleted.");
    }

    public void OnSaveBoxEmptyClicked(SaveBoxEmpty saveBoxEmpty)
    {
        // Check if it's the save menu canvas
        if (saveBoxEmpty.transform.parent == saveMenuCanvas)
        {
            PlaceFilledSaveBox(saveBoxEmpty);
            SavePlayerData();
        }
        // If it's the load menu canvas, do nothing
        else if (saveBoxEmpty.transform.parent == loadMenuCanvas)
        {
            Debug.Log("This is the load menu canvas. No action needed.");
        }
    }


    public void OnSaveBoxFilledClicked(SaveBoxFilled saveBoxFilled)
    {
        if (saveBoxFilled.transform.parent == saveMenuCanvas) // Check if it's the save menu
        {
            OverwriteSaveData(saveBoxFilled);
        }
        else if (saveBoxFilled.transform.parent == loadMenuCanvas) // Check if it's the load menu
        {
            LoadPlayerData();
        }
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

        if (saveBoxFilled.transform.parent == saveMenuCanvas) // Check if it's the save menu
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
        else if (saveBoxFilled.transform.parent == loadMenuCanvas) // Check if it's the load menu
        {
            // Instantiate an empty save box in the same position as the filled save box
            GameObject emptySaveBox = Instantiate(saveBoxEmptyPrefab, saveBoxFilled.transform.position, saveBoxFilled.transform.rotation, loadMenuCanvas);

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

    private void FindScoreAndCoinText()
    {

        if (hudCanvas != null)
        {
            // Now that you have found the HUDCanvas, you can find scoreText and coinText within it
            Transform scoreTextTransform = hudCanvas.Find("ScoreText");
            Transform coinTextTransform = hudCanvas.Find("CoinText");

            if (scoreTextTransform != null)
            {
                Text scoreText = scoreTextTransform.GetComponent<Text>();
                if (scoreText != null)
                {
                    Debug.Log("Score Text Value: " + scoreText.text);
                }
                else
                {
                    Debug.LogWarning("Score Text not found in HUDCanvas.");
                }
            }
            else
            {
                Debug.LogWarning("Score Text Transform not found in HUDCanvas.");
            }

            if (coinTextTransform != null)
            {
                Text coinText = coinTextTransform.GetComponent<Text>();
                if (coinText != null)
                {
                    Debug.Log("Coin Text Value: " + coinText.text);
                }
                else
                {
                    Debug.LogWarning("Coin Text not found in HUDCanvas.");
                }
            }
            else
            {
                Debug.LogWarning("Coin Text Transform not found in HUDCanvas.");
            }
        }
        else
        {
            Debug.LogWarning("HUDCanvas not found among siblings of parent canvas.");
        }
    }

}
