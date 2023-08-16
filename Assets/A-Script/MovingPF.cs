using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPF : MonoBehaviour
{
    [SerializeField] private GameObject[] wayPoint;

    private int currentWayPointIndex = 0;

    [SerializeField] private int speed;

    public bool isPlatform;
    void Update()
    {
        if (Vector2.Distance(wayPoint[currentWayPointIndex].transform.position, transform.position) < .1f)
        {
            currentWayPointIndex++;
            if (currentWayPointIndex == wayPoint.Length)
            {
                currentWayPointIndex = 0;
            }
        }

        transform.position = Vector2.MoveTowards(transform.position,
            wayPoint[currentWayPointIndex].transform.position, Time.deltaTime * speed);
    }
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
