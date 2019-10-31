using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upright : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] public bool holdUpright = true;
    [SerializeField] private float upwardForce = 20;
    [SerializeField] private float downwardForce = 10; // keep downward force

    //[SerializeField] private float yOffset = 1.5f;

    //[SerializeField] public bool isPelvis;
    //[SerializeField] private bool isGrounded = true;
    //[SerializeField] private float risingModifier = 20f;
    //[SerializeField] private float sinkingModifier = 20f;
    //[SerializeField] private float rayDistance = 1f;


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
                transform.position,
                //new Vector3(transform.position.x, transform.position.y + yOffset, transform.position.z),
                ForceMode.Force);

            rb.AddForceAtPosition( Vector3.down * downwardForce,
                transform.position,
                //new Vector3(transform.position.x, transform.position.y - yOffset, transform.position.z), *** Don't know if this makes any difference ***
                ForceMode.Force);

        }
        //else
        //{
        //    rb.AddForce(Vector3.down * sinkingModifier, ForceMode.Force);
        //}


        //if (isPelvis)
        //{
        //    if (Physics.Raycast(rb.transform.position, -transform.forward, rayDistance, 1 << 9))
        //    {
        //        Debug.DrawRay(rb.transform.position, -transform.forward * rayDistance, Color.red);
        //        if (!isGrounded)
        //        {
        //            isGrounded = true;
        //            holdUpright = true;
        //        }
        //        rb.AddForce(Vector3.up * risingModifier, ForceMode.Force); //TODO use raycast normal to add forces instead of "up"
        //    }
        //    else
        //    {
        //        Debug.DrawRay(rb.transform.position, -transform.forward * rayDistance, Color.green);
        //        if (isGrounded)
        //        {
        //            isGrounded = false;
        //            //holdUpright = false;
        //        }
        //        rb.AddForce(Vector3.down * sinkingModifier, ForceMode.Force);
        //    }
        //}

    }

}
