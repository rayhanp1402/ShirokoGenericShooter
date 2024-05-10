using Nightmare;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject settingsMenu;
    public GameObject backButtonOptions;

    public GameObject backButtonStats;

    public GameObject statsMenu;

    void Start()
    {
        // Ensure the settings menu and back button are initially hidden
        if (settingsMenu != null)
        {
            settingsMenu.SetActive(false);
        }
        if (backButtonOptions != null)
        {
            backButtonOptions.SetActive(false);
        }
        if (statsMenu != null)
        {
            statsMenu.SetActive(false);
        }
        if (backButtonStats != null)
        {
            backButtonStats.SetActive(false);
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
        InitialLevel.setLevel(3);
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
            if (backButtonOptions != null)
            {
                backButtonOptions.SetActive(isSettingsMenuActive);
            }
        }
    }

    public void ToggleStatsMenu()
    {
        if (statsMenu != null)
        {
            bool isStatsMenuActive = !statsMenu.activeSelf;
            statsMenu.SetActive(isStatsMenuActive);
            // Toggle visibility of the back button based on the settings menu's visibility
            if (backButtonStats != null)
            {
                backButtonStats.SetActive(isStatsMenuActive);
            }
        }
    }

    // Additional methods for setting player name, volume, and difficulty can be added here
}