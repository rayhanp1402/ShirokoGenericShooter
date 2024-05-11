using UnityEngine;

public class ObjectiveLevel4 : ObjectiveManager
{
    protected override void OnSuccess()
    {
        Debug.Log("All objectives complete!");
    }
}