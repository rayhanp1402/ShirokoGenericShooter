using UnityEngine;
using TMPro;

public class StatsManager : MonoBehaviour{
    public TMP_Text PlayTime; 
    public TMP_Text Distance;
    public TMP_Text Accuracy;
    public TMP_Text Kill;

    private void start(){
        DisplayStats();
    }

    void DisplayStats(){
        if (PlayerPrefs.HasKey("PlayTime"))
        {
            float playTime = PlayerPrefs.GetFloat("PlayTime");
            int hours = Mathf.FloorToInt(playTime / 3600);
            int minutes = Mathf.FloorToInt((playTime % 3600) / 60);
            int seconds = Mathf.FloorToInt(playTime % 60);
            PlayTime.text = "Play Time: " + hours.ToString("00") + ":" + minutes.ToString("00") + ":" + seconds.ToString("00");
        }

        if (PlayerPrefs.HasKey("Distance"))
        {
            float distance = PlayerPrefs.GetFloat("Distance");
            Distance.text = "Distance: " + distance.ToString("F2") + " km";
        }

        if (PlayerPrefs.HasKey("Accuracy"))
        {
            float accuracy = PlayerPrefs.GetFloat("Accuracy");
            Accuracy.text = "Accuracy: " + accuracy.ToString("F2") + "%";
        }

        if (PlayerPrefs.HasKey("Kill"))
        {
            int kill = PlayerPrefs.GetInt("Kill");
            Kill.text = "Kill: " + kill.ToString();
        }
    }
}