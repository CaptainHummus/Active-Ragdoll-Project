using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform playerMagnet;
    [SerializeField] private float distanceToPlayer;


    [SerializeField] private float mouseX;
    [SerializeField] private float mouseY;
    [SerializeField] private float mouseScroll;
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
        mouseScroll = Input.GetAxis("Mouse ScrollWheel");

        playerMagnet.rotation = Quaternion.Euler(rotationX += mouseY * mouseSensitivity, rotationY += mouseX * mouseSensitivity, 0f);

        CheckZoomDistance();

        //Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked ;

    }

    private void LateUpdate()
    {
        //playerMagnet.position = playerTransform.position;
        playerMagnet.DOMove(playerTransform.position, 1f);
        transform.DOLocalMoveZ(-distanceToPlayer, 1f);
    }

    private void CheckZoomDistance()
    {
        if (distanceToPlayer < 1)
        {
            distanceToPlayer = 1;
            return;
        }
        else if (distanceToPlayer > 10)
        {
            distanceToPlayer = 10;
            return;
        }
        distanceToPlayer -= mouseScroll * mouseSensitivity;

    }
}
