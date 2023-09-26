using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : EnemyBase
{
    [SerializeField] private WayPoint index;

    void Update()
    {
        UpdateAnim();
        FlipX();
    }

    public override void UpdateAnim()
    {
        if (isDead)
        {
            anim.Play("BirdDead");
        }
    }

    public override void EnemyHurt()
    {
        isDead = true;
        isMoving = false;
        rg.velocity = new Vector2(rg.velocity.x, 8);
        gameObject.GetComponent<Collider2D>().enabled = false;
        gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
    }

    private void FlipX()
    {
        if (index.currentWayPointIndex % 2 == 0)
        {
            sprite.flipX = false;
        }

        if (index.currentWayPointIndex % 2 == 1)
        {
            sprite.flipX = true;
        }
    }
}