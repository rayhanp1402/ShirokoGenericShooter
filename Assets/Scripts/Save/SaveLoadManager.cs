using UnityEngine;

public class SaveLoadManager : MonoBehaviour
{
    public GameObject saveBoxEmptyPrefab;
    public GameObject saveBoxFilledPrefab;
    public Transform saveMenuCanvas;

    public void OnSaveBoxEmptyClicked(SaveBoxEmpty saveBoxEmpty)
    {
        // Place a filled save box as a child of the SaveMenuCanvas
        saveBoxEmpty.PlaceFilledSaveBox(saveMenuCanvas);
    }

    public void OnSaveBoxClicked()
    {
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
