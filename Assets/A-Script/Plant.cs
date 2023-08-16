using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : EnemyBase
{
    
    protected override Vector2 Direction
    {
        get
        {
            if (!sprite.flipX)
            {
                return new Vector2(-1, 0);
            }
            else
            {
                return new Vector2(1, 0);
            }
        }
    }

    void Update()
    {
        UpdateAnim();
    }

    public override void UpdateAnim()
    {
        //base.UpdateAnim();
        if (isDead)
        {
            anim.Play("PlantDead");
        }
    }

    public override void EnemyHurt()
    {
        base.EnemyHurt();
        rg.constraints = RigidbodyConstraints2D.None;
    }
    
    
}
