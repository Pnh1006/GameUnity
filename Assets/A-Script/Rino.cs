using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rino : EnemyBase
{
    // Start is called before the first frame update
    

    // Update is called once per frame
    void Update()
    {
        DistToPlayer();
        UpdateAnim();
    }
    
    public override void UpdateAnim()
    {
        if (isDead && !isMoving)
        {
            anim.Play("RinoDead");
        }

        if (Espeed == 2f && isMoving)
        {
            anim.Play("RinoRun");
        }
    }
}
