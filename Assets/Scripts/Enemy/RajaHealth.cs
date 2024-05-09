using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RajaHealth : EnemyBaseHealth
{
    protected override void Death()
    {
        base.Death();
        DestroyImmediate(gameObject);
    }
}
