using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoint : MonoBehaviour
{
    [SerializeField] private GameObject[] wayPoint;

    public int currentWayPointIndex = 0;

    [SerializeField] private int speed;

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
}
