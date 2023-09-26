using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    private float speedBullet = 10f;
    [SerializeField] private Rigidbody2D rg;
    
    public void Fire(Vector2 direction)
    {
        rg.velocity = direction * speedBullet;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("Player"))
        {
            SpawnPiece();
            gameObject.SetActive(false);
        }
    }

    protected virtual void SpawnPiece()
    {
        
    }
    
}
