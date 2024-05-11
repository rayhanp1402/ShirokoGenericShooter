using System.Collections;
using System.Collections.Generic;
using Nightmare;
using UnityEngine;

public class ObjectiveTarget : MonoBehaviour
{
    public int index;
    private EnemyHealth health;
    // Start is called before the first frame update
    void Start()
    {
        health = GetComponent<EnemyHealth>();
        health.OnDeath += OnDeath;
    }

    private void OnDeath()
    {
        EventManager.TriggerEvent("AddObjective", index);
    }
}
