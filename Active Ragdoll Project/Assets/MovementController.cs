using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    [Header("Forces")]
    [SerializeField] private float forwardsForce = 10;
    [SerializeField] private float upwardForce = 2;
    [SerializeField] private float rotationTorque = 5;
    [SerializeField] private float jumpForce = 5;

    [Header("Bodyparts")]
    public Rigidbody[] thighs;
    public Rigidbody[] knees;
    public Rigidbody[] shins;
    public Rigidbody[] feet;


    private bool isWalking = false;
    private float paceTick;
    private GameObject camera;
    private Rigidbody rb;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        camera = Camera.main.gameObject;
    }



    private void FixedUpdate()
    {
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

        if (Input.GetKey(KeyCode.A))
        {
            FootForward(1);

            Debug.Log("Left Foot forward");

        }
        if (Input.GetKey(KeyCode.D))
        {
            FootForward(0);

            Debug.Log("Right Foot forward");
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            rb.constraints = RigidbodyConstraints.None;
            rb.AddRelativeTorque(Vector3.back * rotationTorque);
            Debug.Log("Rotate right");
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rb.constraints = RigidbodyConstraints.None;
            rb.AddRelativeTorque(Vector3.forward * rotationTorque);
            Debug.Log("Rotate left");
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

    }

    private void FootForward(int index)
    {
        knees[index].AddForce(camera.transform.forward * forwardsForce + camera.transform.up * upwardForce, ForceMode.Impulse);
        feet[index].AddForce(camera.transform.forward * forwardsForce, ForceMode.Impulse);
        Debug.DrawRay(feet[index].transform.position, camera.transform.forward * forwardsForce, Color.red);
    }
}
