using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    [SerializeField] private Animator anim;
    public bool jumpAfterPad;
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            anim.Play("PadJump");
            other.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up*30, ForceMode2D.Impulse);
            jumpAfterPad = true;
        }
    }

    void Anim()
    {
        anim.Play("PadIdle");
    }
}
