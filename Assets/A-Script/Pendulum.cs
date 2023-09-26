using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pendulum : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rg;
    [SerializeField] private float RAngle;
    [SerializeField] private float LAngle;
    [SerializeField] private float firstPush;
    void Start()
    {
        rg.angularVelocity = firstPush;
    }

    // Update is called once per frame
    void Update()
    {
        Push();
    }

    void Push()
    {
        if (transform.rotation.z > 0 && transform.rotation.z < RAngle && rg.angularVelocity > 0 &&
            rg.angularVelocity < firstPush)
        {
            rg.angularVelocity = firstPush;
        }else if (transform.rotation.z < 0 && transform.rotation.z > LAngle && rg.angularVelocity < 0 &&
                  rg.angularVelocity > -firstPush)
        {
            rg.angularVelocity = -firstPush;
        }
    }
}

