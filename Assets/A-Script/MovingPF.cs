using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPF : MonoBehaviour
{
    public bool isPlatform;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.transform.SetParent(transform);
            isPlatform = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.transform.SetParent(null);
            isPlatform = false;
        }
    }
}
