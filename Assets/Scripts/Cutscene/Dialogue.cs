using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    public string name;
    public GameObject prefab; 
    public string[] sentences;

    public Dialogue(string name, GameObject prefab, string[] sentences)
    {
        this.name = name;
        this.prefab = prefab;
        this.sentences = sentences;
    }
}
