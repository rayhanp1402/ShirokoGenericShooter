using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Statistic : MonoBehaviour
{
    public TMP_Text PlayTime; 
    public TMP_Text Distance;
    public TMP_Text Score;
    public TMP_Text Accuracy;
    public TMP_Text Kill;
    public TMP_Text Death;
    private float playTime;
    public bool isPaused = false; 
    private float totalDistance;
    private Vector3 lastPosition;

     void Start()
    {
        lastPosition = transform.position;
    }

    void Update()
    {
        if (!isPaused)
        {
            playTime += Time.deltaTime;

            int hours = Mathf.FloorToInt(playTime / 3600);
            int minutes = Mathf.FloorToInt((playTime % 3600) / 60);
            int seconds = Mathf.FloorToInt(playTime % 60);

            PlayTime.text = "Play Time: " + hours.ToString("00") + ":" + minutes.ToString("00") + ":" + seconds.ToString("00");

            CalculateDistance();
        }
    }

    void CalculateDistance()
    {
        float distance = Vector3.Distance(transform.position, lastPosition);
        totalDistance += distance;
        lastPosition = transform.position;

        float distanceInKm = totalDistance / 1000f; 

        Distance.text = "Distance: " + distanceInKm.ToString("0.00") + " km";
    }
}