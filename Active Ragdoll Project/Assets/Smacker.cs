using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smacker : MonoBehaviour
{
    [SerializeField] private float speed = 100;
    [SerializeField] private bool clockwise = true;
    [SerializeField] private GameObject arm;
    

    void Update()
    {
        if (!clockwise)
        {
            arm.transform.Rotate(Vector3.right * Time.deltaTime * speed);
        }
        else
        {
            arm.transform.Rotate(Vector3.left * Time.deltaTime * speed);
        }

    }
}
