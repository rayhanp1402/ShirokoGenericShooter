using UnityEngine;
using TMPro;

public class StatsManager : MonoBehaviour{
    public static StatsManager instance;

    public float playTime;
    public float distance;
    public float accuracy;
    public int kill;

    void Awake()
    {
        // Singleton pattern implementation
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

}