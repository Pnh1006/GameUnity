using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Duck : EnemyBase
{
    private bool duckJump = false;
    private bool DuckisGround;
    [SerializeField] private Collider2D coll;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private GameObject[] wayPoint;
    private bool canJump;

    void Update()
    {
        DuckisGround = Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, groundLayer);
        if (transform.position.x <= wayPoint[0].transform.position.x)
        {
            isMovingR = true;
        }

        if (transform.position.x >= wayPoint[1].transform.position.x)
        {
            isMovingR = false;
        }

        if (DuckisGround)
        {
            if (isMoving)
            {
                if (isMovingR)
                {
                    sprite.flipX = true;
                    rg.velocity = new Vector2(5, 14);
                }
                else
                {
                    sprite.flipX = false;
                    rg.velocity = new Vector2(-5, 14);
                }

                isMoving = false;
                StartCoroutine(Wait(1.5f));
            }
        }

        if (rg.velocity.y > 0 && !DuckisGround && !isDead)
        {
            anim.Play("DuckJump");
        }
        else if (rg.velocity.y < 0 && !DuckisGround && !isDead)
        {
            anim.Play("DuckFall");
        }

        if (DuckisGround)
        {
            anim.Play("DuckIdle");
        }

        if (isDead)
        {
            anim.Play("DuckDead");
        }
    }

    private IEnumerator Wait(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        isMoving = true;
    }
}