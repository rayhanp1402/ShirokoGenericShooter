using System.Collections;
using System.Collections.Generic;
using Nightmare;
using UnityEngine;

public class Shopkeeper : PausibleObject
{
    // Start is called before the first frame update
    void Start()
    {
        StartPausible();
    }

    void OnDestroy()
    {
        StopPausible();
    }
}
