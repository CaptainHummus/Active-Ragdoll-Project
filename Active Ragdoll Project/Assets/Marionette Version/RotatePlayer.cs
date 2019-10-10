using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePlayer : MonoBehaviour
{
    private Rigidbody rb;
    public float rotationSpeed;

    Vector3 rotationRight;
    Vector3 rotationLeft;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        rotationLeft.Set(0f, -rotationSpeed, 0f);
        rotationRight.Set(0f, rotationSpeed, 0f);

        rotationLeft = -rotationLeft.normalized * -rotationSpeed;
        rotationRight = rotationRight.normalized * rotationSpeed;

        Quaternion deltaRotationLeft = Quaternion.Euler(rotationLeft * Time.fixedDeltaTime);
        Quaternion deltaRotationRight = Quaternion.Euler(rotationRight * Time.fixedDeltaTime);

        if (Input.GetKey(KeyCode.Q))
        {
            rb.MoveRotation(rb.rotation * deltaRotationLeft);
        }
        if (Input.GetKey(KeyCode.E))
        {
            rb.MoveRotation(rb.rotation * deltaRotationRight);
        }
    }

}
