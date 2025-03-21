using UnityEngine;
using System;
using System.IO;
using TMPro;
using UnityEngine.UI;
using Nightmare;
using System.Collections.Generic;

public class SaveLoadManager : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject saveBoxEmptyPrefab;
    public GameObject saveBoxFilledPrefab;
    public Transform saveMenuCanvas;
    public Transform loadMenuCanvas;
    public Transform hudCanvas;

    private LevelManager levelManager;

    private string saveDirectory;
    private string saveFileExtension = ".txt";

    private static int currentIndex = 1;

    [System.Serializable]
    public class SaveData
    {
        public Vector3 position;
        public float health;
        public List<PetData> pets;
        public int level;
        public string score;
        public string coin;
        public string name; 
        public string time; 

        public SaveData()
        {
            pets = new List<PetData>();
        }
    }

    [Serializable]
    public class PetData
    {
        public string type;
    }


    private void Start()
    {
        if (this.transform.parent == saveMenuCanvas) // Check if it's the save menu
        {
            Button backButton = saveMenuCanvas.Find("BackButton").GetComponent<Button>();
            backButton.onClick.AddListener(HideSaveMenu);
        }
        else if (this.transform.parent == loadMenuCanvas) // Check if it's the load menu
        {
            Button backButton = loadMenuCanvas.Find("BackButton").GetComponent<Button>();
            backButton.onClick.AddListener(HideSaveMenu);
        }
        
        // Define the directory where save files will be stored
        saveDirectory = Application.persistentDataPath + "/SaveData/";
        if (!Directory.Exists(saveDirectory))
        {
            Directory.CreateDirectory(saveDirectory);
        }

        // List all the files in the directory
        string[] saveFiles = Directory.GetFiles(saveDirectory);

        // Determine how many save files exist
        int saveCount = saveFiles.Length;

        Debug.Log("Number of save files found: " + saveCount);

        Vector3 canvasCenter = this.transform.parent.transform.position; // Get the center position of the canvas
        float totalHeight = 0f; // Keep track of the total height occupied by existing save boxes

        // Instantiate save boxes based on the count
        for (int i = 0; i < saveCount; i++)
        {
            // Determine the file name (excluding the directory path)
            string fileName = Path.GetFileName(saveFiles[i]);

            // Read the contents of the JSON file
            string jsonData = File.ReadAllText(saveFiles[i]);

            // Deserialize the JSON data into SaveData object
            SaveData saveData = JsonUtility.FromJson<SaveData>(jsonData);

            // Instantiate SaveBoxFilled for each save file
            GameObject saveBoxPrefab = saveBoxFilledPrefab;
            GameObject saveBox = Instantiate(saveBoxPrefab, this.transform.parent);

            // Calculate the position based on the total height
            float yPosition = canvasCenter.y - totalHeight + 38;

            saveBox.transform.position = new Vector3(canvasCenter.x, yPosition, canvasCenter.z);
            saveBox.transform.rotation = Quaternion.identity;

            SaveBoxFilled saveBoxFilledScript = saveBox.AddComponent<SaveBoxFilled>();
            saveBoxFilledScript.saveLoadManager = this;

            // Set the save name text
            TextMeshProUGUI saveNameText = saveBox.transform.Find("SaveName").GetComponent<TextMeshProUGUI>();
            saveNameText.text = saveData.name;

            // Set the save time text
            TextMeshProUGUI saveTimeText = saveBox.transform.Find("SaveTime").GetComponent<TextMeshProUGUI>();
            saveTimeText.text = saveData.time;

            // Update the total height
            totalHeight += 160f; // Assuming each save box has a height of 160 units
        }

        // Instantiate SaveBoxEmpty for the rest of the slots if max count is 3
        int emptySlots = Mathf.Max(0, 3 - saveCount);

        for (int i = 0; i < emptySlots; i++)
        {
            GameObject emptyBox = Instantiate(saveBoxEmptyPrefab, this.transform.parent);

            // Calculate the position based on the total height
            float yPosition = canvasCenter.y - totalHeight + 38;

            emptyBox.transform.position = new Vector3(canvasCenter.x, yPosition, canvasCenter.z);
            emptyBox.transform.rotation = Quaternion.identity;

            SaveBoxEmpty emptyBoxScript = emptyBox.AddComponent<SaveBoxEmpty>();

            if (emptyBoxScript != null)
            {
                emptyBoxScript.saveLoadManager = this;
                emptyBoxScript.saveBoxFilledPrefab = saveBoxFilledPrefab;
            }

            // Update the total height
            totalHeight += 160f; // Assuming each save box has a height of 160 units
        }

    }



    public void SavePlayerData(SaveBoxEmpty saveBoxEmpty)
    {
        // UpdateSaveUI(saveBoxEmpty);
        Destroy(saveBoxEmpty.gameObject);

        // Instantiate the SaveBoxFilled prefab as a child of the SaveMenuCanvas
        GameObject filledSaveBox = Instantiate(saveBoxFilledPrefab, saveMenuCanvas);

        // Set the position and rotation of the filled save box to match the empty save box
        filledSaveBox.transform.position = saveBoxEmpty.transform.position;
        filledSaveBox.transform.rotation = saveBoxEmpty.transform.rotation;

        SaveBoxFilled saveBoxFilledScript = filledSaveBox.AddComponent<SaveBoxFilled>();
        saveBoxFilledScript.saveLoadManager = this;

        SaveBoxFilled saveBoxFilled = filledSaveBox.GetComponent<SaveBoxFilled>();

        // Call SavePreferences with the SaveBoxFilled type
        UpdateSaveUI(saveBoxFilled);
        SavePreferences(saveBoxFilled);
        Debug.Log("Player data saved.");
    }

    public void SavePlayerData(SaveBoxFilled saveBoxFilled)
    {
        UpdateSaveUI(saveBoxFilled);
        SavePreferences(saveBoxFilled);
        Debug.Log("Player data saved.");
    }

    public void UpdateSaveUI(SaveBoxFilled saveBoxFilled)
    {
        // Update the save time text to the current time
        TextMeshProUGUI saveTimeText = saveBoxFilled.transform.Find("SaveTime").GetComponent<TextMeshProUGUI>();
        if (saveTimeText != null)
        {
            saveTimeText.text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }
        else
        {
            Debug.LogWarning("SaveTime TextMeshProUGUI component not found in SaveBoxFilled prefab.");
        }

        // Set the save name text based on the current index
        TextMeshProUGUI saveNameText = saveBoxFilled.transform.Find("SaveName").GetComponent<TextMeshProUGUI>();
        if (saveNameText != null)
        {
            saveNameText.text = "Save " + currentIndex; 
            currentIndex++;
        }
        else
        {
            Debug.LogWarning("SaveName TextMeshProUGUI component not found in SaveBoxFilled prefab.");
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

    public void SavePreferences(SaveBoxFilled saveBoxFilled)
    {
        // Determine a unique identifier for the save box, such as its name or index
        TextMeshProUGUI saveNameText = saveBoxFilled.transform.Find("SaveName").GetComponent<TextMeshProUGUI>();
        int currlevel = 0;

        string saveFileName = saveNameText.text + saveFileExtension;
        string saveFilePath = Path.Combine(saveDirectory, saveFileName);

        GameObject managersObject = GameObject.Find("Managers");

        if (managersObject != null)
        {
            // Get the LevelManager component attached to the Managers GameObject
            levelManager = managersObject.GetComponent<LevelManager>();

            if (levelManager != null)
            {
                // Access methods or variables from the LevelManager component
                currlevel = levelManager.GetCurrentLevel();
            }
            else
            {
                Debug.LogWarning("LevelManager script component not found on Managers GameObject.");
            }
        }

        float playerHealthValue = 100f;
        Vector3 playerPosition = Vector3.zero;

        GameObject playerPrefab = GameObject.FindWithTag("Player");
        if (playerPrefab != null)
        {
            // Retrieve the player's position
            playerPosition = playerPrefab.transform.position;
            Debug.Log("Player position: " + playerPosition);

            // Get the PlayerHealth component attached to the player prefab
            PlayerHealth playerHealth = playerPrefab.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {

                // Retrieve the player's health
                playerHealthValue = playerHealth.getPlayerHealth();
                Debug.Log("Player health: " + playerHealthValue);
            }
            else
            {
                Debug.LogWarning("PlayerHealth component not found on the player prefab.");
            }
        }
        else
        {
            Debug.LogWarning("Player prefab not found.");
        }

        SaveData saveData = new SaveData();
        saveData.position = playerPosition;
        saveData.health = playerHealthValue;
        saveData.level = currlevel;
        saveData.score = GetScoreValue();
        saveData.coin = GetCoinValue();
        saveData.name = saveNameText.text;
        saveData.time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

        // Retrieve game objects with the "Pet" tag
        GameObject[] pets = GameObject.FindGameObjectsWithTag("Pet");
        foreach (GameObject pet in pets)
        {
            // Check for specific pet behaviour scripts
            AttackerPetBehaviour attackerPet = pet.GetComponent<AttackerPetBehaviour>();
            BufferPetBehaviour bufferPet = pet.GetComponent<BufferPetBehaviour>();
            HealerPetBehaviour healerPet = pet.GetComponent<HealerPetBehaviour>();

            // Determine the pet's type based on the presence of specific script components
            string petType = "Unknown";
            if (attackerPet != null)
            {
                petType = "Attacker";
            }
            else if (bufferPet != null)
            {
                petType = "Buffer";
            }
            else if (healerPet != null)
            {
                petType = "Healer";
            }

            // Add the pet's data to the SaveData object
            saveData.pets.Add(new PetData { type = petType });
        }
        Debug.Log("pets count :" + saveData.pets.Count);

        string jsonData = JsonUtility.ToJson(saveData);

        File.WriteAllText(saveFilePath, jsonData);
    }

    public void LoadPlayerData(SaveBoxFilled saveBoxFilled)
    {
        // Determine the unique identifier for the save box
        TextMeshProUGUI saveNameText = saveBoxFilled.transform.Find("SaveName").GetComponent<TextMeshProUGUI>();
        string saveFileName = saveNameText.text + saveFileExtension;
        string saveFilePath = Path.Combine(saveDirectory, saveFileName);

        if (File.Exists(saveFilePath))
        {
            // Read the data from the file
            string jsonData = File.ReadAllText(saveFilePath);

            // Deserialize the data (for example, using JSON)
            SaveData saveData = JsonUtility.FromJson<SaveData>(jsonData);

            Debug.Log("Player data loaded for save box " + saveFileName + ":");
            Debug.Log("Position: " + saveData.position);
            Debug.Log("Health: " + saveData.health);
            Debug.Log("Level: " + saveData.level);
            Debug.Log("Pets: " + saveData.pets);
            Debug.Log("Score: " + saveData.score);
            Debug.Log("Coin: " + saveData.coin);
            Debug.Log("Name: " + saveData.name);
            Debug.Log("Time: " + saveData.time);
        }
        else
        {
            Debug.LogWarning("No saved data found for " + saveFileName);
        }
    }

    public void DeleteSaveData(SaveBoxFilled saveBoxFilled)
    {
        TextMeshProUGUI saveNameText = saveBoxFilled.transform.Find("SaveName").GetComponent<TextMeshProUGUI>();
        // string saveBoxIdentifier = saveNameText.text;
        string saveFileName = saveNameText.text + saveFileExtension;
        string saveFilePath = Path.Combine(saveDirectory, saveFileName);

        if (File.Exists(saveFilePath))
        {
            // Delete the file
            File.Delete(saveFilePath);

            Debug.Log("Save data deleted for " + saveFileName);
        }
        else
        {
            Debug.LogWarning("No save data found for " + saveFileName);
        }

    }

    public void OnSaveBoxEmptyClicked(SaveBoxEmpty saveBoxEmpty)
    {
        Debug.Log("SaveBoxEmpty clicked.");
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
        Debug.Log("SaveBoxFilled clicked.");
        if (saveBoxFilled.transform.parent == saveMenuCanvas) // Check if it's the save menu
        {
            SavePlayerData(saveBoxFilled);
        }
        else if (saveBoxFilled.transform.parent == loadMenuCanvas) // Check if it's the load menu
        {
            LoadPlayerData(saveBoxFilled);
        }
    }

    // Example method to get the score value from HUDCanvas
    private string GetScoreValue()
    {
        if (hudCanvas != null)
        {
            Transform scoreTextTransform = hudCanvas.Find("ScoreText");
            if (scoreTextTransform != null)
            {
                Text scoreText = scoreTextTransform.GetComponent<Text>();
                if (scoreText != null)
                {
                    // Return only the score value without any additional text
                    return ExtractNumber(scoreText.text);
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
        }
        else
        {
            Debug.LogWarning("HUDCanvas not found among siblings of parent canvas.");
        }
        return "";
    }


    // Example method to get the coin value from HUDCanvas
    private string GetCoinValue()
    {
        if (hudCanvas != null)
        {
            Transform coinTextTransform = hudCanvas.Find("CoinText");
            if (coinTextTransform != null)
            {
                Text coinText = coinTextTransform.GetComponent<Text>();
                if (coinText != null)
                {
                    return coinText.text;
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
        return "";
    }

    private string ExtractNumber(string text)
    {
        // Split the text based on the delimiter ":"
        string[] parts = text.Split(':');

        // Assuming the number is always in the second part after splitting
        if (parts.Length > 1)
        {
            // Extract the numeric part from the second part
            string numericPart = parts[1].Trim(); // Trim to remove leading/trailing whitespaces
            return numericPart;
        }
        else
        {
            return "";
        }
    }

    private int GetSaveCount()
    {
        int count = 0;
        while (PlayerPrefs.HasKey("Save " + count + " _PlayerLevel"))
        {
            count++;
        }
        return count;
    }

    void HideSaveMenu()
    {
        // Hide the save menu canvas
        this.transform.parent.gameObject.SetActive(false);
    }


}
