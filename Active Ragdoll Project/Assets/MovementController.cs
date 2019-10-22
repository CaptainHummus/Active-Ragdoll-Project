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
    private float paceTick = 0f;
    private float pacelength = 2f;

    private float steppyTick = 0f;
    private int steppycounter = 0;

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
        paceTick += Time.fixedDeltaTime;
        steppyTick += Time.fixedDeltaTime;
        if (paceTick >= pacelength)
        {
            paceTick = 0;
        }
    }

    public void InputCheck()
    {

        if (Input.GetKeyDown(KeyCode.A))
        {
            rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
            MoveForward(1);

            Debug.Log("Left Foot forward");

        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
            MoveForward(0);

            Debug.Log("Right Foot forward");
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            rb.constraints = RigidbodyConstraints.None;
            rb.AddRelativeTorque(Vector3.forward * rotationTorque);
            //feet[0].AddRelativeTorque(Vector3.back * rotationTorque/2);
            //feet[1].AddRelativeTorque(Vector3.back * rotationTorque/2);
            SteppySteps('R');
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rb.constraints = RigidbodyConstraints.None;
            rb.AddRelativeTorque(Vector3.back * rotationTorque);
            //feet[1].AddRelativeTorque(Vector3.forward * rotationTorque/2);
            //feet[0].AddRelativeTorque(Vector3.forward * rotationTorque/2);
            SteppySteps('L');
        }
        if (Input.GetKey(KeyCode.Space))
        {
            rb.constraints = RigidbodyConstraints.None;
            //rotate thighs and pelvis in opposite direction to make character crouch a bit
            rb.AddTorque(Vector3.right * rotationTorque / 2);
            thighs[0].AddTorque(Vector3.left * rotationTorque / 2);
            thighs[1].AddTorque(Vector3.left * rotationTorque / 2);
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            MoveForward(1);
            MoveForward(0);
        }
    }

    /// <summary>
    /// moves right or left leg depending on index
    /// </summary>
    /// <param name="index"></param>
    private void MoveForward(int index)
    {
        knees[index].AddForce(cameraObject.transform.forward * forwardsForce + cameraObject.transform.up * forwardsForce, ForceMode.Impulse); 
        feet[index].AddForce(cameraObject.transform.forward * forwardsForce + cameraObject.transform.up * upwardForce, ForceMode.Impulse);
        Debug.DrawRay(feet[index].transform.position, cameraObject.transform.forward * forwardsForce, Color.red, 1f);
    }

    private void SteppySteps(char direction)
    {
        if (steppyTick > 0.10)
        {
            knees[steppycounter % 2].AddForce(cameraObject.transform.up * forwardsForce * 2 + cameraObject.transform.forward, ForceMode.Impulse);
            feet[steppycounter % 2].AddForce(-cameraObject.transform.up * upwardForce * 2, ForceMode.Impulse);
            switch (direction)
            {
                case 'R':
                    feet[steppycounter % 2].AddRelativeTorque(Vector3.forward * rotationTorque / 2, ForceMode.Impulse);
                    break;

                case 'L':
                    feet[steppycounter % 2].AddRelativeTorque(Vector3.back * rotationTorque / 2, ForceMode.Impulse);;
                    break;

                default:
                    Debug.LogError("INVALID ROTATION DIRECTION");
                    break;
            }
            steppycounter++;
            steppyTick = 0;
        }
        switch (direction)
        {
            case 'R':
                feet[1].AddForce(cameraObject.transform.forward * upwardForce + cameraObject.transform.up * forwardsForce);
                break;

            case 'L':
                feet[0].AddForce(cameraObject.transform.forward * upwardForce + cameraObject.transform.up * forwardsForce);
                break;

            default:
                Debug.LogError("INVALID ROTATION DIRECTION");
                break;
        }
    }
}
