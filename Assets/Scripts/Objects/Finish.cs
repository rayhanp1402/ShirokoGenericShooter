using System.Collections;
using System.Collections.Generic;
using Nightmare;
using UnityEngine;

public class Finish : MonoBehaviour
{

    LevelManager levelManager;

    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            levelManager.AdvanceLevel();
        }
    }
}
