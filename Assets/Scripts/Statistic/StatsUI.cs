using UnityEngine;
using TMPro;
using System.Collections.Generic;



public class StatsUI : MonoBehaviour{
    public TMP_Text playTimeText;
    public TMP_Text distanceText;
    public TMP_Text accuracyText;
    public TMP_Text killText;

    void Start()
    {
        // Menampilkan statistik saat inisialisasi
        DisplayStatistics();
    }

    void DisplayStatistics()
    {
        // Mendapatkan statistik yang disimpan dari kelas Statistic
        Dictionary<string, string> stats = Statistic.levelStatistics;

        // Menampilkan statistik pada UI
        playTimeText.text = stats["PlayTime"];
        distanceText.text = stats["Distance"];
        accuracyText.text = stats["Accuracy"];
        killText.text =stats["Kill"];
    }

}