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


    private bool walkingForward = false;
    private float paceTick;
    private GameObject cameraObject;
    private Rigidbody rb;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        cameraObject = Camera.main.gameObject;
    }

    private void FixedUpdate()
    {
        InputCheck();
    }

    public void InputCheck()
    {
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

        if (Input.GetKeyDown(KeyCode.A))
        {
            MoveForward(1);

            Debug.Log("Left Foot forward");

        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            MoveForward(0);

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

    /// <summary>
    /// moves right or left leg depending on index
    /// </summary>
    /// <param name="index"></param>
    /// 
    private void MoveForward(int index)
    {
        knees[index].AddForce(cameraObject.transform.forward * forwardsForce, ForceMode.Impulse); 
        feet[index].AddForce(cameraObject.transform.forward * forwardsForce + cameraObject.transform.up * upwardForce, ForceMode.Impulse);
        Debug.DrawRay(feet[index].transform.position, cameraObject.transform.forward * forwardsForce, Color.red, 1f);
    }
}
