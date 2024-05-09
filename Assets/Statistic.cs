using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Statistic : MonoBehaviour
{
    public ShirokoMovement playerMovement;
    public DefaultGun defaultGun;
    public TMP_Text PlayTime; 
    public TMP_Text Distance;
    public TMP_Text Score;
    public TMP_Text Accuracy;
    public TMP_Text Kill;
    public TMP_Text Death;
    private float playTime;
    public bool isPaused = false; 
    private int killCount;

     void Start()
    {
        killCount = 0;
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
        // Format jarak menjadi dua desimal
        float distanceInKilometers = playerMovement.totalDistanceTraveled / 1000f;

        // Format jarak menjadi dua desimal
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
        Kill.text = "Kill: " + killCount.ToString(); // Memperbarui teks pada UI dengan jumlah kill saat ini
    }

    public void IncrementKill()
    {
        killCount++; // Menambah jumlah kill saat musuh mati
    }
    
}