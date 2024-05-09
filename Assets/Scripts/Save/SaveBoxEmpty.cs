using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using System;
using System.Collections.Generic;

public class SaveBoxEmpty : MonoBehaviour, IPointerClickHandler
{
    public SaveLoadManager saveLoadManager;
    public GameObject saveBoxFilledPrefab;

    public void OnPointerClick(PointerEventData eventData)
    {
        saveLoadManager?.OnSaveBoxEmptyClicked(this);
    }

}
