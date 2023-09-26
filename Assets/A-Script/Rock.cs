using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rg;
    [SerializeField] private float accelerate;
    [SerializeField] private float currentSpeed;

    [SerializeField] private Vector2 direction;
    [SerializeField] private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        currentSpeed += accelerate;
        rg.velocity = direction * currentSpeed;
    }
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Turn1"))
        {
            currentSpeed = 0;
            accelerate = 0;
            direction = new Vector2(0, 1);
            anim.Play("RockHitLeft");
        }


        if (other.gameObject.CompareTag("Turn2"))
        {
            currentSpeed = 0;
            accelerate = 0;
            direction = new Vector2(1, 0);
            anim.Play("RockHitTop");
        }

        if (other.gameObject.CompareTag("Turn3"))
        {
            currentSpeed = 0;
            accelerate = 0;
            direction = new Vector2(0, -1);
            anim.Play("RockHitRight");
        }

        if (other.gameObject.CompareTag("Turn4"))
        {
            accelerate = 0;
            currentSpeed = 0;
            direction = new Vector2(-1, 0);
            anim.Play("RockHitUnder");
        }
    }

    void Idle()
    {
        anim.Play("RockIdle");
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.3f);
        accelerate = 0.05f;
        yield return new WaitForSeconds(0.5f);
        accelerate = 0.1f;
    }
}