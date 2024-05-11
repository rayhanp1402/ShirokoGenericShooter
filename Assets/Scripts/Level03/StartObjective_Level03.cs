using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartObjective_Level03 : MonoBehaviour
{
    GameObject blockades;
    ObjectiveManager objectiveManager;
    GameObject enemyManagers;
    // Start is called before the first frame update

    void Awake()
    {
        blockades = GameObject.Find("Blockades");
        objectiveManager = FindObjectOfType<ObjectiveManager>();
        enemyManagers = GameObject.Find("EnemyManagers");
    }
    
    void Start()
    {
        blockades.SetActive(false);
        objectiveManager.gameObject.SetActive(false);
        enemyManagers.SetActive(false);
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Player");
            objectiveManager.gameObject.SetActive(true);
            Destroy(this);
        }
    }
}
