using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pig : EnemyBase
{
    void Update()
    {
        DistToPlayer();
        UpdateAnim();
    }
}