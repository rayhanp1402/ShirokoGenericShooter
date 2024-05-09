using UnityEngine;
using UnityEngine.EventSystems;

public class SaveBoxFilled : MonoBehaviour, IPointerClickHandler
{
    // Reference to the SaveLoadManager script
    public SaveLoadManager saveLoadManager;

    // Called when the save box is clicked
    public void OnPointerClick(PointerEventData eventData)
    {
        // Check if the SaveLoadManager script is assigned
        if (saveLoadManager != null)
        {
            // Call a method in the SaveLoadManager to handle the click event
            saveLoadManager.OnSaveBoxClicked();
        }
    }
}
