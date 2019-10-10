using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterUpright : MonoBehaviour
{
    protected Rigidbody rb;
    public bool keepUpright = true;
    public float uprightForce = 10;
    public float uprightOffset = 1.45f;
    public float additionalUpwardForce = 10;
    public float damnpenAngularForce = 0;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.maxAngularVelocity = 40; // CANNOT APPLY HIGH ANGULAR FORCE UNLESS THE MAXANGULAR VELOCITY IS INCREASED
    }

    private void FixedUpdate()
    {
        if (keepUpright)
        {
            // USE TWO FORCES PULLING UP AND DOWN AT THE TOP AND BOTTOM OF THE OBJECT RESPECTIVELY TO PULL IT UPRIGHT
            // THIS TEQNIQUE CAN BE USED FOR PULLING AN OBJECT TO FACE ANY VECTOR

            rb.AddForceAtPosition(new Vector3(0, (uprightForce + additionalUpwardForce), 0),
                transform.position + transform.TransformPoint(new Vector3(0, uprightOffset, 0)), ForceMode.Force);

            rb.AddForceAtPosition(new Vector3(0, -uprightForce, 0),
                transform.position + transform.TransformPoint(new Vector3(0, uprightOffset, 0)), ForceMode.Force);
        }
        if (damnpenAngularForce > 0)
        {
            rb.angularVelocity *= (1 - Time.deltaTime * damnpenAngularForce);
        }
    }
}
