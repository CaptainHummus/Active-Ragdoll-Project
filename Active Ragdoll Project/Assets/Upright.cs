using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upright : MonoBehaviour
{
    [SerializeField] private bool holdUpright = true;
    [SerializeField] private float upwardForce = 20;
    [SerializeField] private float downwardForce = 10; // keep downward force
    [SerializeField] private float yOffset = 1.5f;


    [SerializeField] private Rigidbody rb;



    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.maxAngularVelocity = 100; // 
    }

    private void FixedUpdate()
    {
        if (holdUpright)
        {
            rb.AddForceAtPosition( Vector3.up * upwardForce,
                transform.position + transform.TransformPoint(Vector3.up * yOffset),
                ForceMode.Force); // Upward force and position

            rb.AddForceAtPosition( Vector3.down * downwardForce,
                transform.position + transform.TransformPoint(Vector3.down * yOffset),
                ForceMode.Force); // Upward force and position

        }
    }

}
