using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour
{
    [SerializeField] private GameObject obj;

    public void spawnObject()
    {
        float x = Random.Range(-1, 1);
        float z = Random.Range(-1, 1);
        
        Instantiate(obj,new Vector3(x,2,z), Random.rotation);
    }
}
