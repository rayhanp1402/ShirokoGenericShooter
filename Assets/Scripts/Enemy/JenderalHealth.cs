using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nightmare;

public class JenderalHealth : EnemyHealth
{
    JenderalAttack jenderalAttack;

    protected override void Awake()
    {
        base.Awake();
        jenderalAttack = GetComponent<JenderalAttack>();
    }

    protected override void Death()
    {
        base.Death();
        jenderalAttack.enabled = false;
        // DestroyImmediate(gameObject);
    }
}
