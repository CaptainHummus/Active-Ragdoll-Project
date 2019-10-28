using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform playerMagnet;
    [SerializeField] private float distanceToPlayer;


    [SerializeField] private float mouseX;
    [SerializeField] private float mouseY;
    [SerializeField] private float rotationX;
    [SerializeField] private float rotationY;
    [SerializeField] private float mouseSensitivity;




    private void Start()
    {
        playerMagnet = transform.root;
    }

    private void Update()
    {
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");

        playerMagnet.rotation = Quaternion.Euler(rotationX += mouseY * mouseSensitivity, rotationY += mouseX * mouseSensitivity, 0f);
        //Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked ;
    }

    private void LateUpdate()
    {
        playerMagnet.position = playerTransform.position;
    }
}
