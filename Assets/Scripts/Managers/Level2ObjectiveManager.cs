using Nightmare;
using UnityEngine;

public class Level2ObjectiveManager : ObjectiveManager{
    private new LevelManager levelManager;

    protected override void OnSuccess()
    {
        levelManager.AdvanceLevel();
    }
}