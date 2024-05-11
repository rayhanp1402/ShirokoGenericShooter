using Nightmare;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level2ObjectiveManager : ObjectiveManager{
    private new LevelManager levelManager;

    private GameObject statsMenu;
    private Statistic stats;

    protected override void OnSuccess()
    {
        // levelManager.AdvanceLevel();
        statsMenu = GameObject.FindGameObjectWithTag("Stats");
        stats = statsMenu.GetComponent<Statistic>();
        stats.SaveStatistics();

        SceneManager.LoadScene("MainMenu");
    }
}