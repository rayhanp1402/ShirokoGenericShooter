using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverManager : MonoBehaviour
{
    public float countdownDuration = 10f;
    public TextMeshProUGUI timerText; 
    private float countdownTimer; 

    private void Start()
    {
        countdownTimer = countdownDuration;
    }

    private void Update()
    {
        // Update the countdown timer
        countdownTimer -= Time.deltaTime;

        // Update the UI text to display the remaining time
        UpdateTimerUI();

        // Check if the countdown timer has reached zero
        if (countdownTimer <= 0)
        {
            // Move to the main menu scene
            LoadMainMenu();
        }
    }

    private void UpdateTimerUI()
    {
        // Update the UI text to display the remaining time
        timerText.text = Mathf.CeilToInt(countdownTimer).ToString();
    }

    public void Retry()
    {
        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        // Load the main menu scene
        LoadMainMenu();
    }

    private void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
