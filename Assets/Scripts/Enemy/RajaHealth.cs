using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nightmare;


public class RajaHealth : EnemyHealth
{
    RajaAttack rajaAttack;

    protected override void Awake()
    {
        base.Awake();
        rajaAttack = GetComponent<RajaAttack>();
    }
    protected override void Death()
    {
        base.Death();
        rajaAttack.enabled = false;
        // DestroyImmediate(gameObject);
    }
}
