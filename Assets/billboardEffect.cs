using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardEffect : MonoBehaviour
{

    Camera mainCamera;
    RectTransform rectTransform;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion cameraRotation = mainCamera.transform.rotation;
        rectTransform.rotation = cameraRotation;
    }
}
