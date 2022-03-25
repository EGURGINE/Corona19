using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    protected override void Attack()
    {
        BossAttack(Random.Range(1, 4));
    }
}
