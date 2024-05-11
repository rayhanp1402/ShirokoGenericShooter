using UnityEngine;

public class TestObjectiveManager : ObjectiveManager
{
    protected override void OnSuccess()
    {
        Debug.Log("All objectives complete!");
    }
}