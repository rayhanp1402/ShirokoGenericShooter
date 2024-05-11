using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshGen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        MeshFilter[] meshes = transform.GetComponentsInChildren<MeshFilter>();
        for (int i = 0; i < meshes.Length; i++)
        {
            Mesh mesh = meshes[i].mesh;
            GameObject go = meshes[i].gameObject;
            MeshCollider tmp = go.AddComponent<MeshCollider>();
            tmp.sharedMesh = mesh;
        }
    }


}
