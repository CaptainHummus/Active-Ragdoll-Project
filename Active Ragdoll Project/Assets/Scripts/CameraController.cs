using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform playerMagnet;
    [SerializeField] private float distanceToPlayer;
    [SerializeField] private float mouseSensitivity;

    private float mouseX;
    private float mouseY;
    private float mouseScroll;
    private float rotationX;
    private float rotationY;

    private void Start()
    {
        playerMagnet = transform.root;
        DOTween.SetTweensCapacity(500, 50); //done because Dotween warned me about too low capacity
    }

    private void Update()
    {
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");
        mouseScroll = Input.GetAxis("Mouse ScrollWheel");
        rotationX = Mathf.Clamp(rotationX, -15f, 90f);
        playerMagnet.rotation = Quaternion.Euler(rotationX += mouseY * Time.deltaTime * mouseSensitivity, rotationY += mouseX * Time.deltaTime * mouseSensitivity, 0f);

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
        distanceToPlayer -= mouseScroll * Time.deltaTime * mouseSensitivity;
        distanceToPlayer = Mathf.Clamp(distanceToPlayer, 1f, 10f);

    }
}
