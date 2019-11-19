using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private float force = 100;
    private float killTimer = 5f;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.up * force, ForceMode.Impulse);
        Destroy(gameObject, killTimer);
    }
}
