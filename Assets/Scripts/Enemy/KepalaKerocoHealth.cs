using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KepalaKerocoHealth : EnemyBaseHealth
{
    protected override void Death()
    {
        base.Death();
        DestroyImmediate(gameObject);
    }
}
