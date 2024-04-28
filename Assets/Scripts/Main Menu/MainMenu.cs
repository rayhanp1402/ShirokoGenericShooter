using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MainMenu : MonoBehaviour
{

    void Start()
    {
        // Find the SettingsMenu GameObject
        GameObject settingsMenu = GameObject.Find("SettingsMenu");

        // Check if the SettingsMenu is found
        if (settingsMenu != null)
        {
            // Find the button inside the SettingsMenu
            Button buttonToHide = settingsMenu.GetComponentInChildren<Button>();

            // Check if the button is found
            if (buttonToHide != null)
            {
                // Hide the button
                buttonToHide.gameObject.SetActive(false);
            }
            else
            {
                Debug.LogWarning("Button not found in SettingsMenu!");
            }
        }
        else
        {
            Debug.LogWarning("SettingsMenu not found!");
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

}
