using UnityEngine;

public class ObjectiveLevel1 : ObjectiveManager
{
    Transform player;
    override protected void Start()
    {
        base.Start();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    protected override void OnSuccess()
    {
        CoinDropManager.DropMoney(player.position, 30);
        Debug.Log("Objective 1 complete");
    }
}