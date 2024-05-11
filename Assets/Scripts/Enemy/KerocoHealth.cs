using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Nightmare;

public class KerocoHealth : EnemyHealth
{
    KerocoAttack kerocoAttack;

    protected override void Awake()
    {
        base.Awake();
        kerocoAttack = GetComponent<KerocoAttack>();
    }

    protected override void Death()
    {
        base.Death();
        kerocoAttack.enabled = false;
        anim.SetTrigger("Die");

        Debug.Log("Keroco is dead");
        // DestroyImmediate(gameObject);
    }
}
