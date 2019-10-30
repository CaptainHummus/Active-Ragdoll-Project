using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upright : MonoBehaviour
{
    [SerializeField] public bool holdUpright = true;
    [SerializeField] private bool isGrounded = true;
    [SerializeField] private float upwardForce = 20;
    [SerializeField] private float downwardForce = 10; // keep downward force
    //[SerializeField] private float yOffset = 1.5f;
    [SerializeField] private float risingModifier = 20f;
    [SerializeField] private float sinkingModifier = 20f;

    [SerializeField] private float rayDistance = 1f;



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
                transform.position,
                //new Vector3(transform.position.x, transform.position.y + yOffset, transform.position.z),
                ForceMode.Force);

            rb.AddForceAtPosition( Vector3.down * downwardForce,
                transform.position,
                //new Vector3(transform.position.x, transform.position.y - yOffset, transform.position.z), *** Don't know if this makes any difference ***
                ForceMode.Force);

            // TODO: Correct the raycast to follow the angle of pelvis instead of vector3.down
            if (Physics.Raycast(rb.transform.position, Vector3.down, rayDistance, 1 << 9))
            {
                Debug.DrawRay(rb.transform.position, Vector3.down * rayDistance, Color.red);
                if (!isGrounded)
                {
                    isGrounded = true;

                }
                rb.AddForce(Vector3.up * risingModifier, ForceMode.Force); //TODO use raycast normal to add forces instead of "up"
            }
            else
            {
                Debug.DrawRay(rb.transform.position, Vector3.down * rayDistance, Color.green);
                if (isGrounded)
                {
                    isGrounded = false;
                }
                rb.AddForce(Vector3.down * sinkingModifier, ForceMode.Force);
            }
        }

    }

}
