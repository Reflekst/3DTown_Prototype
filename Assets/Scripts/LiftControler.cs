using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftControler : MonoBehaviour
{
    [SerializeField] private GameObject Player;

    [SerializeField] private float speed;

    [SerializeField] private Transform zero, first, second, liftPosition;

    [SerializeField] private bool isInsideTrigger = false;

    private Vector3 destinationTarget, departTarget;

    public static int called;

    private bool isReady = true;


    void Start()
    {
        departTarget = zero.position;
        destinationTarget = zero.position;
    }

    void FixedUpdate()
    {
        ChooseFloor();
    }

    private void ChooseFloor()
    {
        if (isReady)
        {
            if (isInsideTrigger)
            {
                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    destinationTarget = zero.position;
                    Move();
                    isReady = false;
                }
                else if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    destinationTarget = first.position;
                    Move();
                    isReady = false;
                    
                }
                else if (Input.GetKeyDown(KeyCode.Alpha3))
                {
                    destinationTarget = second.position;
                    Move();
                    isReady = false;
                }
            }
            if (called > 0)
            {
                isReady = false;
                switch (called)
                {
                    case 1:
                        destinationTarget = zero.position;
                        break;
                    case 2:
                        destinationTarget = first.position;
                        break;
                    case 3:
                        destinationTarget = second.position;
                        break;
                    default:
                        break;
                }
                called = 0;
                Move();
            }
            
        }
        else
        {
            CheckisReady();
        }
    }

    private void CheckisReady()
    {
        if (Vector3.Distance(transform.position, destinationTarget) <= 0.01f)
        {
            isReady = true;
        }
    }

    private void Move()
    {
        transform.position = Vector3.Lerp(departTarget, destinationTarget, speed);
        departTarget = destinationTarget;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == Player)
        {
            Player.transform.parent = null;
            isInsideTrigger = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == Player)
        {
            Debug.Log("Wcisnij 1 lub 2 lub 3");
            Player.transform.parent = transform;
            isInsideTrigger = true;
        }
    }

}
