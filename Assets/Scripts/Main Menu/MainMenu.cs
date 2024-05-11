using Nightmare;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject settingsMenu;
    public GameObject backButton;

    void Start()
    {
        // Ensure the settings menu and back button are initially hidden
        if (settingsMenu != null)
        {
            settingsMenu.SetActive(false);
        }
        if (backButton != null)
        {
            backButton.SetActive(false);
        }
    }

    public void PlayGame()
    {
        // Load the next scene
        InitialLevel.setLevel(0);
        SceneManager.LoadScene("Main");
        Debug.Log("Loading next scene...");
    }

    public void LoadGame()
    {
        // TODO: Implement loading saved game functionality
        // Load a specific level
        InitialLevel.setLevel(5);
        SceneManager.LoadScene("Main");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ToggleSettingsMenu()
    {
        if (settingsMenu != null)
        {
            bool isSettingsMenuActive = !settingsMenu.activeSelf;
            settingsMenu.SetActive(isSettingsMenuActive);
            // Toggle visibility of the back button based on the settings menu's visibility
            if (backButton != null)
            {
                backButton.SetActive(isSettingsMenuActive);
            }
        }
    }

    // Additional methods for setting player name, volume, and difficulty can be added here
}