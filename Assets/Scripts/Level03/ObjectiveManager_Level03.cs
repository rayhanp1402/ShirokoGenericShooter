using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveManager_Level03 : ObjectiveManager
{
    GameObject blockades;
    GameObject enemyManagers;
    Transform player;
    Canvas canvas;

    void Awake()
    {
        blockades = GameObject.Find("Blockades");
        player = GameObject.FindGameObjectWithTag("Player").transform;
        enemyManagers = GameObject.Find("EnemyManagers");
    }

    override protected void OnEnable()
    {
        enemyManagers.SetActive(true);
        blockades.SetActive(true);
    }

    override protected void OnDisable()
    {
        enemyManagers.SetActive(false);
        blockades.SetActive(false);
    }

    protected override void OnSuccess()
    {
        blockades.SetActive(false);
        enemyManagers.SetActive(false);
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }
        CoinDropManager.DropMoney(player.position, 1000);
        Destroy(this.gameObject);
    }
}
