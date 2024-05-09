using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cheat : MonoBehaviour
{
    public GameObject textBox;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            textBox.SetActive(!textBox.activeSelf);
        }
    }
}
