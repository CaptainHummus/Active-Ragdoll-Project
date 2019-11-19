using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MovingPLatform : MonoBehaviour
{
    [SerializeField] private Vector3 startPos;
    [SerializeField] private Vector3 endPos;
    [SerializeField] private float duration = 5f;
    private bool isStartPosition = true;


    void Start()
    {
        startPos = transform.position;
        MovePlatform();
    }

    void MovePlatform()
    {
        if (isStartPosition)
        {
            transform.DOMove(endPos, duration);
            isStartPosition = false;
            StartCoroutine("Wait", duration);
        }
        else
        {
            transform.DOMove(startPos, duration);
            isStartPosition = true;
            StartCoroutine("Wait", duration);
        }

    }

    IEnumerator Wait(float time)
    {
        yield return new WaitForSeconds(time);
        MovePlatform();
    }

}
