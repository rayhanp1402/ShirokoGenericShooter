using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject settingsMenu;
    public GameObject backButton;

    public GameObject statsMenu;

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
        if (statsMenu != null)
        {
            statsMenu.SetActive(false);
        }
    }

    public void PlayGame()
    {
        // Load the next scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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

    public void ToggleStatsMenu()
    {
        if (statsMenu != null)
        {
            bool isStatsMenuActive = !statsMenu.activeSelf;
            statsMenu.SetActive(isStatsMenuActive);
            // Toggle visibility of the back button based on the settings menu's visibility
            if (backButton != null)
            {
                backButton.SetActive(isStatsMenuActive);
            }
        }
    }

    // Additional methods for setting player name, volume, and difficulty can be added here
}