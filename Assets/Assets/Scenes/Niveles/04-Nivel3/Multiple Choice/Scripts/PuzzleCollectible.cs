using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleCollectible : MonoBehaviour
{
    public Transform doorTarget;
    public float moveSpeed = 5f;
    public float rotateSpeed = 200f;

    bool isMoving = false;
    bool counted = false; 

    void Update()
    {
        if (isMoving && doorTarget != null)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                doorTarget.position,
                moveSpeed * Time.deltaTime
            );

            transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);

            if (!counted && Vector3.Distance(transform.position, doorTarget.position) < 0.1f)
            {
                counted = true;

                FindObjectOfType<DoorController>().AddObject();
            }
        }
    }

    public void Activate()
    {
        isMoving = true;
    }
}