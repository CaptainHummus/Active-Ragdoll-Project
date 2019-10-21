using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    [Header("Forces")]
    [SerializeField] private float forwardsForce = 10;
    [SerializeField] private float upwardForce = 2;

    [Header("Bodyparts")]
    public Rigidbody[] thighs;
    public Rigidbody[] knees;
    public Rigidbody[] shins;
    public Rigidbody[] feet;


    private bool isWalking = false;
    private float paceTick;
    private GameObject camera;


    private void Start()
    {
        camera = Camera.main.gameObject;
    }

    private void Update()
    {
        if (Input.GetKey("a"))
        {
            FootForward(1);

            Debug.Log("Left Foot forward");

        }
        if (Input.GetKey("d"))
        {
            FootForward(0);

            Debug.Log("Right Foot forward");
        }
        //else
        //{
        //    isWalking = false;
        //}
    }


    //private void FixedUpdate()
    //{
    //    paceTick = paceTick + Time.fixedDeltaTime;
    //    if (isWalking)
    //    {
    //        if (paceTick > 0.5)
    //        {
    //            FootForward(1);
    //            paceTick = 0f;
    //        }
    //    }
    //}

    private void FootForward(int index)
    {
        knees[index].AddForce(camera.transform.forward * forwardsForce + camera.transform.up * upwardForce, ForceMode.Impulse);
        feet[index].AddForce(camera.transform.forward * forwardsForce, ForceMode.Impulse);
        Debug.DrawRay(feet[index].transform.position, camera.transform.forward * forwardsForce, Color.red);
    }
}
