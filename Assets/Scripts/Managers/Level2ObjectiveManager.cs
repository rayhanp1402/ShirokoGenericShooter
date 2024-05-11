using Nightmare;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level2ObjectiveManager : ObjectiveManager{
    GameObject finish;
    protected override void Start()
    {
        base.Start();
        finish = FindObjectOfType<Finish>().gameObject;
        finish.SetActive(false);
    }
    protected override void OnSuccess()
    {
        finish.SetActive(true);
        CoinDropManager.DropMoney(GameObject.FindGameObjectWithTag("Player").transform.position, 400);
    }
}