using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class KerocoHealth : EnemyBaseHealth
{
    protected override void Death()
    {
        base.Death();
        DestroyImmediate(gameObject);
    }
}
