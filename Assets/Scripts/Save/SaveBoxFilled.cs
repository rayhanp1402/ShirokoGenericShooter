using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SaveBoxFilled : MonoBehaviour, IPointerClickHandler
{
    public SaveLoadManager saveLoadManager;

    void Start()
    {
        // Find the DeleteButton child of SaveBoxFilled
        Button deleteButton = GetComponentInChildren<Button>();
        if (deleteButton != null)
        {
            // Add an onClick listener to the deleteButton and assign OnDeleteButtonClicked as the function to call
            deleteButton.onClick.AddListener(OnDeleteButtonClicked);
        }
        else
        {
            Debug.LogWarning("DeleteButton not found in SaveBoxFilled.");
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // Check if the SaveLoadManager script is assigned
        if (saveLoadManager != null)
        {
            // Call a method in the SaveLoadManager to handle the click event
            saveLoadManager.OnSaveBoxClicked();
        }
    }

    public void OnDeleteButtonClicked()
    {
        // Check if the SaveLoadManager script is assigned
        if (saveLoadManager != null)
        {
            // Call a method in the SaveLoadManager to handle the delete button click event
            saveLoadManager.OnDeleteButtonClicked(this, saveLoadManager);
        }
    }
}
