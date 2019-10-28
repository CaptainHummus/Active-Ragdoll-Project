using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform playerMagnet;
    [SerializeField] private float distanceToPlayer;


    private void Start()
    {
        playerMagnet = transform.root;
    }

    private void Update()
    {
        playerMagnet.position = playerTransform.position;

    }
}
