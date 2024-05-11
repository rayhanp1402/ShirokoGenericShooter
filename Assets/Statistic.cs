using Nightmare;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Statistic : MonoBehaviour
{ 
    private PlayerMovement playerMovement;
    private GameObject player;
    private DefaultGun defaultGun;
    public TMP_Text PlayTime; 
    public TMP_Text Distance;
    public TMP_Text Accuracy;
    public TMP_Text Kill;
    public TMP_Text Death;
    private float playTime;
    public bool isPaused = false; 
    private int killCount;

    public static Dictionary<string, string> levelStatistics = new Dictionary<string, string>();

     void Start()
    {
        killCount = 0;
        player = GameObject.FindGameObjectWithTag("Player");
        defaultGun = player.GetComponentInChildren<DefaultGun>();
        
        playerMovement = player.GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if (!isPaused)
        {
            playTime += Time.deltaTime;
            UpdatePlayTime();
            UpdateDistance();
            UpdateAccuracy();
            UpdateKill();
        }
    }
    
    void UpdatePlayTime()
    {
        int hours = Mathf.FloorToInt(playTime / 3600);
        int minutes = Mathf.FloorToInt((playTime % 3600) / 60);
        int seconds = Mathf.FloorToInt(playTime % 60);
        PlayTime.text = "Play Time: " + hours.ToString("00") + ":" + minutes.ToString("00") + ":" + seconds.ToString("00");
    }

    void UpdateDistance()
    {
        float distanceInKilometers = playerMovement.totalDistanceTraveled / 1000f;

        Distance.text = "Distance: " + distanceInKilometers.ToString("F2") + " km"; 
    }

    void UpdateAccuracy()
    {
        if (defaultGun.shotsFired > 0)
        {

            float accuracy = (float)defaultGun.shotsHit / defaultGun.shotsFired * 100;
            Accuracy.text = "Accuracy: " + accuracy.ToString("F2") + "%" + " (" + defaultGun.shotsHit + "/" + defaultGun.shotsFired + ")";
        }
    }

    void UpdateKill()
    {
        Kill.text = "Kill: " + killCount.ToString();
    }

    public void IncrementKill()
    {
        killCount++;
    }   

    public void SaveStatistics()
    {
        levelStatistics.Clear();
        levelStatistics.Add("PlayTime", PlayTime.text);
        levelStatistics.Add("Distance", Distance.text);
        levelStatistics.Add("Accuracy", Accuracy.text);
        levelStatistics.Add("Kill", Kill.text);
    }
}