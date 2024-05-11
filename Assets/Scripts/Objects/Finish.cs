using System.Collections;
using System.Collections.Generic;
using Nightmare;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{

    LevelManager levelManager;

    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
        Debug.Log(SceneManager.GetActiveScene().buildIndex.ToString());
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            levelManager.AdvanceLevel();
        }
    }
}
