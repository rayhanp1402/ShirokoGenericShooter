using UnityEngine;

public class SaveLoadManager : MonoBehaviour
{
    public GameObject saveBoxEmptyPrefab;
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

    public void OnDeleteButtonClicked(SaveBoxFilled saveBoxFilled, SaveLoadManager saveLoadManager)
    {
        // Instantiate an empty save box in the same position as the filled save box
        GameObject emptySaveBox = Instantiate(saveBoxEmptyPrefab, saveBoxFilled.transform.position, saveBoxFilled.transform.rotation, saveMenuCanvas);

        SaveBoxEmpty emptyBoxScript = emptySaveBox.AddComponent<SaveBoxEmpty>();

        if (emptyBoxScript != null)
        {
            emptyBoxScript.saveLoadManager = saveLoadManager;
            emptyBoxScript.saveBoxFilledPrefab = saveBoxFilledPrefab;
        }
        // Destroy the filled save box
        Destroy(saveBoxFilled.gameObject);
    }
}
