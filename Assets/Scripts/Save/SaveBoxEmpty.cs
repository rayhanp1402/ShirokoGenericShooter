using UnityEngine;
using UnityEngine.EventSystems;

public class SaveBoxEmpty : MonoBehaviour, IPointerClickHandler
{
    // Reference to the SaveLoadManager script
    public SaveLoadManager saveLoadManager;

    // Reference to the SaveBoxFilled prefab
    public GameObject saveBoxFilledPrefab;

    // Called when the save box is clicked
    public void OnPointerClick(PointerEventData eventData)
    {
        // Check if the SaveLoadManager script is assigned
        if (saveLoadManager != null)
        {
            // Call a method in the SaveLoadManager to handle the click event
            saveLoadManager.OnSaveBoxEmptyClicked(this);
        }
    }

    // Method to replace the empty save box with a filled one
    public void PlaceFilledSaveBox(Transform parent)
    {
        // Instantiate the SaveBoxFilled prefab as a child of the specified parent
        GameObject filledSaveBox = Instantiate(saveBoxFilledPrefab, parent);

        // Set the position and rotation of the filled save box to match the empty save box
        filledSaveBox.transform.position = transform.position;
        filledSaveBox.transform.rotation = transform.rotation;

        // Add the SaveBoxFilled script to the instantiated GameObject
        SaveBoxFilled saveBoxFilledScript = filledSaveBox.AddComponent<SaveBoxFilled>();

        SaveLoadManager saveLoadManager = FindObjectOfType<SaveLoadManager>();

        // Assign the SaveLoadManager component to the SaveBoxFilled script
        if (saveLoadManager != null)
        {
            saveBoxFilledScript.saveLoadManager = saveLoadManager;
        }
        else
        {
            Debug.LogWarning("SaveLoadManager not found in the scene.");
        }

        // Destroy the empty save box
        Destroy(gameObject);
    }


}
