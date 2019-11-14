using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MovementController : MonoBehaviour
{
    [Header("Forces")]
    [SerializeField] private float forwardsForce = 10;
    [SerializeField] private float upwardForce = 2;
    [SerializeField] private float rotationTorque = 5;
    [SerializeField] private float jumpForce = 5;
    [SerializeField] private float jumpCharge;

    [Header("Bodyparts")]
    public Rigidbody[] thighs;
    public Rigidbody[] knees;
    public Rigidbody[] shins;
    public Rigidbody[] feet;
    public Rigidbody head;

    [Header("Upright Raycast Mods")]
    [SerializeField] private float risingModifier = 20f;
    [SerializeField] private float sinkingModifier = 20f;
    [SerializeField] private float rayDistance = 1f;
    private bool isGrounded;

    [Header ("Misc")]
    public Upright[] uprightComponents;
    [SerializeField] private float pacelength = 2f;
    private bool walkingForward;
    private bool walkingBackward;
    private bool sprinting;
    private float paceTick = 0f;

    private float steppyTick = 0f;
    private int steppycounter = 0;

    private GameObject cameraObject;
    [SerializeField] private Rigidbody rb;


    private void Start()
    {
        //rb = GetComponent<Rigidbody>();
        cameraObject = Camera.main.gameObject;
    }
    private void Update()
    {
        InputCheck();
    }

    private void FixedUpdate()
    {
        steppyTick += Time.fixedDeltaTime;
        PaceSequence();
        GroundCheck();
    }

    public void InputCheck()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (!sprinting)
            {
                forwardsForce *= 2;
                sprinting = true;
            }

        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            if (sprinting)
            {
                forwardsForce /= 2;
                sprinting = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
            walkingForward = true;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
            walkingBackward = true;
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            //rb.constraints = RigidbodyConstraints.None;
            walkingForward = false;

        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            //rb.constraints = RigidbodyConstraints.None;
            walkingBackward = false;

        }
        if (Input.GetKey(KeyCode.A))
        {
            rb.constraints = RigidbodyConstraints.None;
            rb.AddRelativeTorque(Vector3.back * rotationTorque * Time.deltaTime);
            SteppySteps('L');
            rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        }
        if (Input.GetKey(KeyCode.D))
        {
            rb.constraints = RigidbodyConstraints.None;
            rb.AddRelativeTorque(Vector3.forward * rotationTorque * Time.deltaTime);
            SteppySteps('R');
            rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        }
        if (Input.GetKey(KeyCode.Space))
        {
            //rb.constraints = RigidbodyConstraints.None;
            rb.AddTorque(Vector3.back * rotationTorque  *Time.deltaTime);             //rotate leg parts and pelvis in opposite directions to make character crouch a bit
            thighs[0].AddTorque(Vector3.forward * rotationTorque * Time.deltaTime);
            thighs[1].AddTorque(Vector3.forward * rotationTorque * Time.deltaTime);
            shins[0].AddTorque(Vector3.back * rotationTorque * Time.deltaTime);
            shins[1].AddTorque(Vector3.back * rotationTorque * Time.deltaTime);
            feet[0].AddTorque(Vector3.forward * rotationTorque * Time.deltaTime);
            feet[1].AddTorque(Vector3.forward * rotationTorque * Time.deltaTime);
            jumpCharge += Time.deltaTime;
            jumpCharge = Mathf.Clamp(jumpCharge, 0f, 1.5f);
            ToggleUpright(false);
        }
        if (Input.GetKeyUp(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce * jumpCharge, ForceMode.Impulse);
            rb.AddForce(-rb.transform.up * jumpForce * jumpCharge, ForceMode.Impulse);
            feet[1].AddForce(-transform.up * jumpForce * jumpCharge/10, ForceMode.Impulse);
            feet[0].AddForce(-transform.up * jumpForce * jumpCharge/10, ForceMode.Impulse);
            jumpCharge = 0f;
            rb.constraints = RigidbodyConstraints.None;
            ToggleUpright(true);
        }
        else if (Input.GetKeyUp(KeyCode.Space) && !isGrounded)
        {
            ToggleUpright();
            jumpCharge = 0f;
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ToggleUpright();
        }
    }

    private void PaceSequence()
    {
        float magnitude = 1f;
        if (paceTick > pacelength * magnitude)
        {
            paceTick = 0;
        }

        #region Walk Forwards
        if (walkingForward)
        {
            paceTick += Time.fixedDeltaTime;
            if (paceTick > 0.9 * magnitude)
            {
                feet[0].AddForce(Vector3.down * upwardForce * 2, ForceMode.Impulse);
                //pause between steps and set foot down
            }
            else if (paceTick > 0.5 * magnitude)
            {
                //Adding forces from the pelvises "forward" and "upward"
                knees[1].AddForce(-transform.up * forwardsForce + transform.forward * upwardForce * 2, ForceMode.Impulse);
                feet[1].AddForce(-transform.up * forwardsForce + transform.forward * upwardForce, ForceMode.Impulse);
                feet[0].AddForce(-feet[0].transform.up * upwardForce, ForceMode.Impulse);

                //Debug.DrawRay(feet[1].transform.position, transform.forward, Color.red, 1f);
            }
            else if (paceTick > 0.4 * magnitude)
            {
                feet[1].AddForce(Vector3.down * upwardForce * 2, ForceMode.Impulse);
                //pause between steps and set foot down
            }
            else
            {
                knees[0].AddForce(-transform.up * forwardsForce + transform.forward * upwardForce * 2, ForceMode.Impulse);
                feet[0].AddForce(-transform.up * forwardsForce + transform.forward * upwardForce, ForceMode.Impulse);
                feet[1].AddForce(-feet[1].transform.up * upwardForce, ForceMode.Impulse);

                //Debug.DrawRay(feet[0].transform.position, transform.forward, Color.red, 1f);
            }
            rb.AddForce(rb.transform.forward * upwardForce, ForceMode.Impulse);
        }
        #endregion

        #region Backwards Walk
        if (walkingBackward)
        {
            paceTick += Time.fixedDeltaTime;
            if (paceTick > 0.8 * magnitude)
            {
                //pause between steps
            }
            else if (paceTick > 0.5 * magnitude)
            {
                //Adding forces from the pelvises "forward" and "upward"
                knees[1].AddForce(transform.up * forwardsForce + transform.forward * forwardsForce, ForceMode.Impulse);
                shins[1].AddForce(transform.forward * upwardForce, ForceMode.Impulse);
                //feet[0].AddForce(feet[0].transform.up * upwardForce, ForceMode.Impulse);

                //Debug.DrawRay(feet[1].transform.position, transform.forward, Color.red, 1f);
            }
            else if (paceTick > 0.3 * magnitude)
            {
                //pause between steps
            }
            else
            {
                knees[0].AddForce(transform.up * forwardsForce + transform.forward * forwardsForce, ForceMode.Impulse);
                shins[0].AddForce(transform.forward * upwardForce, ForceMode.Impulse);
                //feet[1].AddForce(feet[0].transform.up * upwardForce, ForceMode.Impulse);

                //Debug.DrawRay(feet[0].transform.position, transform.forward, Color.red, 1f);
            }
            //rb.AddForce(-rb.transform.forward * upwardForce, ForceMode.Impulse);
        }
        #endregion


    }

    private void OldMoveForward(int index)
    {
        knees[index].AddForce(cameraObject.transform.forward * forwardsForce + cameraObject.transform.up * forwardsForce, ForceMode.Impulse); 
        feet[index].AddForce(cameraObject.transform.forward * forwardsForce + cameraObject.transform.up * upwardForce, ForceMode.Impulse);
        Debug.DrawRay(feet[index].transform.position, cameraObject.transform.forward * forwardsForce, Color.red, 1f);
    }

    // This method needs improvement to make rotational steps clearer
    private void SteppySteps(char direction)
    {
        if (steppyTick > 0.20)
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

    private void ToggleUpright()
    {
        foreach (Upright bodypart in uprightComponents)
        {
            bodypart.holdUpright = !bodypart.holdUpright;
        }
    }

    private void ToggleUpright(bool mode)
    {
        //rb.constraints = RigidbodyConstraints.None;
        foreach (Upright bodypart in uprightComponents)
        {
            bodypart.holdUpright = mode;
        }
    }

    //TODO: slowly tilt pelvis to normal angle of floor.

    private void GroundCheck()
    {
        if (Physics.Raycast(rb.transform.position, -transform.forward, out RaycastHit hit, rayDistance, 1 << 9))
        {
            Debug.DrawRay(rb.transform.position, -transform.forward * rayDistance, Color.red);
            if (!isGrounded)
            {
                isGrounded = true;
                ToggleUpright(true);
            }

            //Debug.Log("hit: " + hit.normal);
            //Debug.Log("hit rad2deg: " + hit.normal * Mathf.Rad2Deg);
            rb.AddForce(Vector3.up * risingModifier, ForceMode.Force);
        }
        else
        {
            Debug.DrawRay(rb.transform.position, -transform.forward * rayDistance, Color.green);
            if (isGrounded)
            {
                isGrounded = false;
                //ToggleUpright(false);
            }
            rb.AddForce(Vector3.down * sinkingModifier, ForceMode.Force);
        }

        //if (Physics.Raycast(rb.transform.position, Vector3.down * 1.2f, out RaycastHit hit2, rayDistance, 1 << 9))
        //{
        //    //Debug.Log("hit2: " + hit2.normal);
        //    //Debug.Log("hit2 rad2deg: " + hit2.normal * Mathf.Rad2Deg);
        //    //transform.localRotation = Quaternion.FromToRotation(transform.up, hit2.normal) * transform.rotation;


        //    //rb.constraints = RigidbodyConstraints.FreezeRotationZ;

        //    rb.transform.DOLookAt(hit.point, 2f, AxisConstraint.None, Vector3.right);
        //    //rb.DOLookAt(hit.point, 1f, AxisConstraint.X, Vector3.forward);

        //    Debug.Log("Balancing pelvis rotation");

        //    //rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        //}
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.relativeVelocity.magnitude > 3)
        {
            //Debug.Log("col magnitude: " + collision.relativeVelocity.magnitude);
            ToggleUpright(false);
            head.AddForce(Vector3.down * jumpForce * 0.2f, ForceMode.Impulse);
        }
    }
}
