using UnityEngine;

public class ObjectiveLevel1 : ObjectiveManager
{
    Transform player;
    GameObject finish;
    override protected void Start()
    {
        base.Start();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        finish = FindObjectOfType<Finish>().gameObject;
        finish.SetActive(false);
    }
    protected override void OnSuccess()
    {
        CoinDropManager.DropMoney(player.position, 250);
        finish.SetActive(true);
    }
}