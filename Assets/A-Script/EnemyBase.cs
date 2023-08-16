using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    [SerializeField] protected Rigidbody2D rg;
    protected bool isMovingR = true;
    protected bool isRunning;
    protected bool isMoving = true;
    protected float Espeed;

    [SerializeField] protected int indexBullet;
    [SerializeField] protected SpriteRenderer sprite;
    [SerializeField] private Transform player;
    [SerializeField] private float distCheck;
    [SerializeField] protected Animator anim;

    [SerializeField] protected Transform bulletPosition;

    [SerializeField] private Vector2 direction;

    protected virtual Vector2 Direction => direction;

    public bool isDead = false;

    public virtual void EnemyHurt()
    {
        isDead = true;
        isMoving = false;
        rg.velocity = new Vector2(rg.velocity.x, 8);
        gameObject.GetComponent<Collider2D>().enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Turn"))
        {
            if (isMovingR)
            {
                isMovingR = false;
            }
            else
            {
                isMovingR = true;
            }
        }
    }

    public virtual void EnemyMoving(float speed)
    {
        if (isMoving)
        {
            if (isMovingR)
            {
                sprite.flipX = true;
                rg.velocity = new Vector2(speed, rg.velocity.y);
            }
            else
            {
                sprite.flipX = false;
                rg.velocity = new Vector2(-speed, rg.velocity.y);
            }
        }

        Espeed = speed;
    }

    public virtual void DistToPlayer()
    {
        float distToPlayer = Vector2.Distance(transform.position, player.position);
        if (distToPlayer < distCheck && (player.position.y - transform.position.y >= 0) &&
            (player.position.y - transform.position.y <= 1))
        {
            isRunning = true;
            if (player.position.x < transform.position.x)
            {
                isMovingR = false;
            }
            else
            {
                isMovingR = true;
            }

            EnemyMoving(4f);
        }
        else
        {
            isRunning = false;
            EnemyMoving(2f);
        }
    }

    public virtual void UpdateAnim()
    {
        if (isDead && !isMoving)
        {
            anim.Play("PigHit2");
        }

        if (Espeed == 2f && isMoving)
        {
            anim.Play("PigWalk");
        }

        if (Espeed == 4f && isMoving)
        {
            anim.Play("PigRun");
        }
    }

    public void Dead()
    {
        gameObject.SetActive(false);
    }

    protected IEnumerator WaitStunDead()
    {
        yield return new WaitForSeconds(0.5f);
        Dead();
    }

    public virtual void Fire()
    {
        Bullet bullet = ObjectPool.instance.GetPoolObject(indexBullet).GetComponent<Bullet>();
        if (bullet != null)
        {
            bullet.transform.position = bulletPosition.position;
            bullet.gameObject.SetActive(true);
            bullet.Fire(Direction);
        }
    }
}