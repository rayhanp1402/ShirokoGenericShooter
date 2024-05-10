using UnityEngine;
using UnityEngine.SceneManagement;

public class ObjectiveManager : MonoBehaviour
{
    public int totalEnemies; // Total number of enemies to defeat
    private int enemiesDefeated; // Number of enemies defeated

    // private void Start()
    // {
    //     // Count the total number of enemies at the start of the scene
    //     CountTotalEnemies();
    // }

    // private void CountTotalEnemies()
    // {
    //     // Find all GameObjects with specific tags and count them
    //     GameObject[] rajaEnemies = GameObject.FindGameObjectsWithTag("Raja");
    //     GameObject[] kerocoEnemies = GameObject.FindGameObjectsWithTag("Keroco");
    //     GameObject[] jenderalEnemies = GameObject.FindGameObjectsWithTag("Jenderal");
    //     GameObject[] kepalaKerocoEnemies = GameObject.FindGameObjectsWithTag("KepalaKeroco");

    //     totalEnemies = rajaEnemies.Length + kerocoEnemies.Length + jenderalEnemies.Length + kepalaKerocoEnemies.Length;
    //     Debug.Log("Total enemies: " + totalEnemies);
    // }

    // Call this method when an enemy is defeated
    public void EnemyDefeated()
    {
        Debug.Log("Enemy defeated: ");
        
        enemiesDefeated++;

        // Check if all enemies are defeated
        if (enemiesDefeated >= totalEnemies)
        {
            // Load the next scene
            LoadNextScene();
        }
    }

    // Load the next scene
    private void LoadNextScene()
    {
        Debug.Log("Loading next scene...");
        // Get the index of the next scene in the build settings
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        SceneManager.LoadScene("MainMenu");

        // // Check if the next scene exists
        // if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        // {
        //     // Load the next scene
        //     SceneManager.LoadScene("MainMenu");
        // }
        // else
        // {
        //     Debug.LogWarning("No next scene found.");
        // }
    }
}
