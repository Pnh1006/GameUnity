using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bee : EnemyBase
{
    void Update()
    {
        UpdateAnim();
    }

    public override void UpdateAnim()
    {
        //base.UpdateAnim();
        if (isDead)
        {
            anim.Play("BeeDead");
        }
    }

    public override void EnemyHurt()
    {
        base.EnemyHurt();
        rg.constraints = RigidbodyConstraints2D.None;
    }
}