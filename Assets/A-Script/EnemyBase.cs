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
    protected float distToPlayer;
    protected Vector2 iniPos;

    [SerializeField] private int indexFlip;

    [SerializeField] protected string nameBullet;
    [SerializeField] protected SpriteRenderer sprite;
    [SerializeField] protected Transform player;
    [SerializeField] protected float distCheck;
    [SerializeField] protected Animator anim;
    [SerializeField] protected Transform bulletPosition;
    [SerializeField] private Vector2 direction;
    [SerializeField] protected Transform turn1;
    [SerializeField] protected Transform turn2;

    protected virtual Vector2 Direction => direction;

    public bool isDead;

    private void Start()
    {
        iniPos = transform.position;
    }

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
            if (indexFlip % 2 == 0)
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
            else
            {
                if (isMovingR)
                {
                    sprite.flipX = false;
                    rg.velocity = new Vector2(-speed, rg.velocity.y);
                }
                else
                {
                    sprite.flipX = true;
                    rg.velocity = new Vector2(speed, rg.velocity.y);
                }
            }
        }

        Espeed = speed;
    }

    public virtual void DistToPlayer()
    {
        distToPlayer = Vector2.Distance(transform.position, player.position);
        if (distToPlayer < distCheck && (player.position.y - transform.position.y >= 0) &&
            (player.position.y - transform.position.y <= 1) && player.position.x > turn1.position.x &&
            player.position.x < turn2.position.x)
        {
            isRunning = true;
            if (indexFlip % 2 == 0)
            {
                if (player.position.x < transform.position.x)
                {
                    isMovingR = false;
                }
                else
                {
                    isMovingR = true;
                }
            }
            else
            {
                if (player.position.x < transform.position.x)
                {
                    isMovingR = true;
                }
                else
                {
                    isMovingR = false;
                }
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

   

    protected IEnumerator WaitStunDead()
    {
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
        // sprite.color = Color.clear;
        // StartCoroutine(Respawn(5f));
    }
    
    // protected IEnumerator Respawn(float waitTime)
    // {
    //     yield return new WaitForSeconds(waitTime);
    //     transform.position = iniPos;
    //     gameObject.GetComponent<Collider2D>().enabled = true;
    //     sprite.color = Color.white;
    //     isDead = false;
    // }

    /// Ham nay goi trong event
    public virtual void Fire()
    {
        Bullet bullet = ObjectPool.instance.GetPoolObject(nameBullet).GetComponent<Bullet>();
        if (bullet != null)
        {
            bullet.transform.position = bulletPosition.position;
            bullet.gameObject.SetActive(true);
            bullet.Fire(Direction);
        }
    }
}