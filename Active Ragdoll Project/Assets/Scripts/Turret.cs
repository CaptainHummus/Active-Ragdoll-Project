using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Turret : MonoBehaviour
{
    [SerializeField] private GameObject turretBase;
    [SerializeField] private GameObject target;
    [SerializeField] private GameObject Bullet;
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private SphereCollider detectionTrigger;
    [SerializeField] private float rangeRadius = 8f;
    [SerializeField] private float fireCooldown = 2f;

    private Vector3 targetRotation;

    private void Start()
    {
        detectionTrigger = GetComponent<SphereCollider>();
        detectionTrigger.radius = rangeRadius;
    }

    private void Update()
    {
        if (target != null)
        {
            transform.DOLookAt(target.transform.position, 1.5f);
            //transform.rotation = Quaternion.LookRotation(target.transform.position - transform.position);
            //transform.DOLocalRotateQuaternion(Quaternion.Euler(target.transform.position - transform.position), 2f);
            //transform.DOLocalRotate(, 2f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Head"))
        {
            Debug.Log("Player has entered turret range");
            target = other.gameObject;
            InvokeRepeating("Fire", fireCooldown, fireCooldown);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Head"))
        {
            Debug.Log("Player has exited turret range");
            target = null;
            CancelInvoke("Fire");
        }
    }

    private void Fire()
    {
        Instantiate(Bullet, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        Debug.Log("FIRE");
    }
}
