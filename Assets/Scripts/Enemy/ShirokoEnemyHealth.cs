using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ShirokoEnemyHealth : EnemyBaseHealth
{
    protected override void Death()
    {
        base.Death();
        anim.SetTrigger("Dead"); 
    }
}
