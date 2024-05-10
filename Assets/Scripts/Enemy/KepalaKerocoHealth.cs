using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nightmare;

public class KepalaKerocoHealth : EnemyHealth
{
    KepalaKerocoAttack kepalaKerocoAttack;

    protected override void Awake()
    {
        base.Awake();
        kepalaKerocoAttack = GetComponent<KepalaKerocoAttack>();
    }

    protected override void Death()
    {
        base.Death();
        kepalaKerocoAttack.enabled = false;
        // DestroyImmediate(gameObject);
    }
}
