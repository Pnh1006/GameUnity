using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fan : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sp;
    private bool isTouching;
    private Vector2 iniPos;

    private void Start()
    {
        iniPos = transform.position;
    }

    private void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player") && (other.transform.position.y - transform.position.y >= 1))
        {
            StartCoroutine(Wait());
        }
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.5f);
        gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        yield return new WaitForSeconds(0.5f);
        gameObject.GetComponent<Collider2D>().enabled = false;
        yield return new WaitForSeconds(1f);
        sp.color = Color.clear;
    }


    private IEnumerator Wait2()
    {
        yield return new WaitForSeconds(2f);
        transform.position = iniPos;
        gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        gameObject.GetComponent<Collider2D>().enabled = true;
        sp.color = Color.white;
        isTouching = false;
    }
}