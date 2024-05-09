using UnityEngine;

public class SaveLoadManager : MonoBehaviour
{
    public GameObject saveBoxFilledPrefab;
    public Transform saveMenuCanvas;

    // Called when the empty save box is clicked
    public void OnSaveBoxEmptyClicked(SaveBoxEmpty saveBoxEmpty)
    {
        // Place a filled save box as a child of the SaveMenuCanvas
        saveBoxEmpty.PlaceFilledSaveBox(saveMenuCanvas);
    }

    // Called when the save box is clicked
    public void OnSaveBoxClicked()
    {
        // Perform the desired action, such as logging a message
        Debug.Log("Save box clicked!");
    }
}
