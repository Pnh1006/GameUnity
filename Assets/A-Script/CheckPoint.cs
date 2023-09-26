using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] private PlayerController player;
    [SerializeField] private Transform respawnPoint;
    [SerializeField] private Animator anim;
    private bool isCheckPoint;
    private void Update()
    {
        anim.Play("CheckPoint2");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player.UpdateCheckPoint(respawnPoint.position);
        }
    }

   
}