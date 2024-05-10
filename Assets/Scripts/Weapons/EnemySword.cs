using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySword : MonoBehaviour
{
    AudioSource fireAudio;
    Light fireLight;
    LineRenderer fireLine;

    Ray fireRay;
    RaycastHit fireHit;
    int shootableMask;


    void Awake()
    {
        fireAudio = GetComponent<AudioSource>();
        fireLight = GetComponent<Light>();
        fireLine = GetComponent<LineRenderer>();

        shootableMask = LayerMask.GetMask("Shootable");
    }

    public void DisableEffects()
    {
        fireLine.enabled = false;
        fireLight.enabled = false;
    }

    public void Shoot(float range)
    {
        fireAudio.Play();
        fireLight.enabled = true;

        fireLine.enabled = true;
        fireLine.SetPosition(0, transform.position);

        fireRay.origin = transform.position;
        fireRay.direction = transform.forward;

        fireLine.SetPosition(1, fireRay.origin + fireRay.direction * range);
    }
}
