using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float speed = 3f;
    public GameObject pointA;
    public GameObject pointB;

    private bool movingToPositionB = true;
    private bool movingToPositionA = false;

    void FixedUpdate()
    {
        MovePlatform();
    }

    private void MovePlatform()
    {
        if(movingToPositionB)
        {
            transform.position = Vector3.MoveTowards(transform.position, pointB.transform.position, speed * Time.deltaTime);
        }
        else if(movingToPositionA)
        {
            transform.position = Vector3.MoveTowards(transform.position, pointA.transform.position, speed * Time.deltaTime);
        }

        if(transform.position == pointA.transform.position)
        {
            movingToPositionB = true;
            movingToPositionA = false;
        }
        else if(transform.position == pointB.transform.position)
        {
            movingToPositionA = true;
            movingToPositionB = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            other.gameObject.transform.parent = this.gameObject.transform;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.transform.parent = null;
        }
    }
}
