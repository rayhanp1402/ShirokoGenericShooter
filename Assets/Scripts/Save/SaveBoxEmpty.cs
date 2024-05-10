using UnityEngine;
using UnityEngine.EventSystems;

public class SaveBoxEmpty : MonoBehaviour, IPointerClickHandler
{
    public SaveLoadManager saveLoadManager;
    public GameObject saveBoxFilledPrefab;

    public void OnPointerClick(PointerEventData eventData)
    {
        saveLoadManager?.OnSaveBoxEmptyClicked(this);
    }

}
