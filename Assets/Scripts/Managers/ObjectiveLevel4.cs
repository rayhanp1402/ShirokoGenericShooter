using UnityEngine;

public class ObjectiveLevel4 : ObjectiveManager
{
    protected override void OnSuccess()
    {
        levelManager.AdvanceLevel();
    }
}