using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;
using Nightmare;

public class SaveLoadManager : MonoBehaviour
{
    public GameObject saveBoxEmptyPrefab;
    public GameObject saveBoxFilledPrefab;
    public Transform saveMenuCanvas;
    public Transform loadMenuCanvas;
    public Transform hudCanvas;

    private LevelManager levelManager;

    private static int currentIndex = 1;

    // Save player data to PlayerPrefs
    public void SavePlayerData(SaveBoxEmpty saveBoxEmpty)
    {
        UpdateSaveUI(saveBoxEmpty);
        SavePreferences();
        Debug.Log("Player data saved.");
    }

    public void SavePlayerData(SaveBoxFilled saveBoxFilled)
    {
        UpdateSaveUI(saveBoxFilled);
        SavePreferences();
        Debug.Log("Player data saved.");
    }

    public void UpdateSaveUI(SaveBoxEmpty saveBoxEmpty)
    {
        // Instantiate the SaveBoxFilled prefab as a child of the SaveMenuCanvas
        GameObject filledSaveBox = Instantiate(saveBoxFilledPrefab, saveMenuCanvas);

        // Set the position and rotation of the filled save box to match the empty save box
        filledSaveBox.transform.position = saveBoxEmpty.transform.position;
        filledSaveBox.transform.rotation = saveBoxEmpty.transform.rotation;

        SaveBoxFilled saveBoxFilledScript = filledSaveBox.AddComponent<SaveBoxFilled>();
        saveBoxFilledScript.saveLoadManager = this;

        // Update the save time text to the current time
        TextMeshProUGUI saveTimeText = filledSaveBox.transform.Find("SaveTime").GetComponent<TextMeshProUGUI>();
        if (saveTimeText != null)
        {
            saveTimeText.text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            // Sample overwrite data
            PlayerPrefs.SetFloat("PlayerHealth", 80f);

            Debug.Log("Save data Saved.");
        }
        else
        {
            Debug.LogWarning("SaveTime TextMeshProUGUI component not found in SaveBoxFilled prefab.");
        }

        // Set the save name text based on the current index
        TextMeshProUGUI saveNameText = filledSaveBox.transform.Find("SaveName").GetComponent<TextMeshProUGUI>();
        if (saveNameText != null)
        {
            saveNameText.text = "Save " + currentIndex; 
            currentIndex++;
        }
        else
        {
            Debug.LogWarning("SaveName TextMeshProUGUI component not found in SaveBoxFilled prefab.");
        }

        // Destroy the empty save box
        Destroy(saveBoxEmpty.gameObject);
    }

    // update save ui for save box filled
    public void UpdateSaveUI(SaveBoxFilled saveBoxFilled)
    {
        // Update the save time text to the current time
        TextMeshProUGUI saveTimeText = saveBoxFilled.transform.Find("SaveTime").GetComponent<TextMeshProUGUI>();
        if (saveTimeText != null)
        {
            saveTimeText.text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            // Sample overwrite data
            PlayerPrefs.SetFloat("PlayerHealth", 80f);

            Debug.Log("Save data Saved.");
        }
        else
        {
            Debug.LogWarning("SaveTime TextMeshProUGUI component not found in SaveBoxFilled prefab.");
        }
    }

    public void DeleteSaveUI(SaveBoxFilled saveBoxFilled, SaveLoadManager saveLoadManager)
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
            SavePlayerData(saveBoxEmpty);
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
            SavePlayerData(saveBoxFilled);
        }
        else if (saveBoxFilled.transform.parent == loadMenuCanvas) // Check if it's the load menu
        {
            LoadPlayerData();
        }
    }


    public void SavePreferences()
    {
        // Save level
        GameObject managersObject = GameObject.Find("Managers");

        if (managersObject != null)
        {
            // Get the LevelManager component attached to the Managers GameObject
            levelManager = managersObject.GetComponent<LevelManager>();

            if (levelManager != null)
            {
                // Access methods or variables from the LevelManager component
                int currlevel = levelManager.GetCurrentLevel();
                Debug.Log("Current Level: " + currlevel);
            }
            else
            {
                Debug.LogWarning("LevelManager script component not found on Managers GameObject.");
            }
        }

        // Find and display score and coin text
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
