using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public abstract class ObjectiveManager : MonoBehaviour
{

    public enum ObjectiveType
    {
        DefeatEnemies,
        Survive,
    }
    public List<Objective> objectives = new List<Objective>();
    private TextMeshProUGUI[] objectivesText;
    private float countdownTimer;
    void Start()
    {
        RectTransform Panel = GameObject.Find("Panel").GetComponent<RectTransform>();
        TextMeshProUGUI text = Panel.Find("Label").GetComponent<TextMeshProUGUI>();
        objectivesText = new TextMeshProUGUI[objectives.Count];
        for (int i = 0; i < objectives.Count; i++)
        {
            objectives[i].currentProgress = 0;
            objectives[i].isComplete = false;
            objectivesText[i] = Instantiate(text, Panel);
            if (objectives[i].objectiveType == ObjectiveType.DefeatEnemies)
            {
                objectivesText[i].text = "Kalahkan " + objectives[i].objectiveValue + " " + objectives[i].objectiveName + " (0/" + objectives[i].objectiveValue + ")";
            }
            else if (objectives[i].objectiveType == ObjectiveType.Survive)
            {
                objectivesText[i].text = "Bertahan selama " + objectives[i].objectiveValue + " detik";
            }
        }
        countdownTimer = 0;
    }

    void Update()
    {
        countdownTimer += Time.deltaTime;
        UpdateObjective();
        if (CheckObjectives())
        {
            OnSuccess();
        }
    }

    void OnEnable()
    {
        EventManager.StartListening("AddObjective", AddObjectiveProgress);
    }

    private void AddObjectiveProgress(int index)
    {
        objectives[index].currentProgress++;
        if (objectives[index].currentProgress >= objectives[index].objectiveValue)
        {
            objectives[index].isComplete = true;
        }
    }

    void OnDisable()
    {
        EventManager.StopListening("AddObjective", AddObjectiveProgress);
    }

    void UpdateObjective(){
        for (int i = 0; i < objectives.Count; i++)
        {
            if (objectives[i].objectiveType == ObjectiveType.DefeatEnemies)
            {
                objectivesText[i].text = "Kalahkan " + objectives[i].objectiveValue + " " + objectives[i].objectiveName + " (" + objectives[i].currentProgress + "/" + objectives[i].objectiveValue + ")";
            }
            else if (objectives[i].objectiveType == ObjectiveType.Survive)
            {
                if (objectives[i].isComplete)
                {
                   objectivesText[i].text = "Bertahan selama " + objectives[i].objectiveValue + " detik (Selesai)";
                } else{
                    objectivesText[i].text = "Bertahan selama " + objectives[i].objectiveValue + " detik (" + countdownTimer.ToString("F1") + "/" + objectives[i].objectiveValue + ")";
                }
                if (countdownTimer >= objectives[i].objectiveValue)
                {
                    objectives[i].isComplete = true;
                }
            }
            if (objectives[i].isComplete)
            {
                objectivesText[i].color = Color.green;
            }
        }
    }

    public bool CheckObjectives()
    {
        for (int i = 0; i < objectives.Count; i++)
        {
            if (!objectives[i].isComplete)
            {
                return false;
            }
        }
        return true;
    }

    protected abstract void OnSuccess();
}

[System.Serializable]
public class Objective
{
    public string objectiveName; 
    public ObjectiveManager.ObjectiveType objectiveType;
    public float objectiveValue;
    public float currentProgress;
    public bool isComplete;
}
