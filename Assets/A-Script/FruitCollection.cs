using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FruitCollection : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Animator anim;
    [SerializeField] private PlayerController player;
    public int coins = 0;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
        {
            anim.Play("Collected");
        }
      
    }

    void Collected()
    {
        gameObject.SetActive(false);
    }
}