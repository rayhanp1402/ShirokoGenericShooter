using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollisionIgnore : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Physics.IgnoreLayerCollision(0, 7);
        Physics.IgnoreLayerCollision(6, 7);
        Physics.IgnoreLayerCollision(7, 7);
    }
}
