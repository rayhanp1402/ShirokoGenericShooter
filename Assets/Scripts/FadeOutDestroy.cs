using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOutDestroy : MonoBehaviour
{

    private bool isFading = false;
    private float fadeTime = 1.0f;
    Renderer[] renderers;

    // Start is called before the first frame update
    void Start()
    {
        renderers = GetComponentsInChildren<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isFading)
        {
            foreach (Renderer r in renderers)
            {
                foreach (Material m in r.materials)
                {
                    Color c = m.color;
                    c.a -= Time.deltaTime / fadeTime;
                    m.color = c;
                }
            }
            if (renderers[0].material.color.a <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    public void StartFadeOut()
    {
        isFading = true;
    }
}
