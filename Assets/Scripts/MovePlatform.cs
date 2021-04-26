using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatform : MonoBehaviour
{
    [SerializeField] private GameObject Player;

    [SerializeField] private float speed;

    [SerializeField] private Transform startPoint, endPoint, currentPoint;

    [SerializeField] private float changeDirectionDelay;

    private Transform destinationTarget, departTarget;

    private float startTime;

    private float journeyLength;

    private bool isWaiting;

    public static bool givenSign = false;

    bool wasSwitched = false;

    void Start()
    {
   
        departTarget = startPoint;
        destinationTarget = endPoint;
        
        startTime = Time.time;
        journeyLength = Vector3.Distance(destinationTarget.position, destinationTarget.position);
    }

    void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        if (!isWaiting)
        {
            if (givenSign)
            {
                ChangeDestination();
            }
            else if (Vector3.Distance(transform.position, destinationTarget.position) > 0.01f)
            {
                float distCovered = (Time.time - startTime) * speed;

                float fractionOfJourney = distCovered / journeyLength;

                transform.position = Vector3.Lerp(departTarget.position, destinationTarget.position, fractionOfJourney);
            }
            else
            {
                isWaiting = true;
                StartCoroutine(changeDelay());
            }
        }

    }
    private void ChangeDestination()
    {
        startTime = Time.time;
        journeyLength = Vector3.Distance(departTarget.position, destinationTarget.position);
        isWaiting = false;

        if (givenSign)
        {
            destinationTarget = departTarget;
            departTarget = currentPoint;
            givenSign = false;
            wasSwitched = true;
        }
        else if (destinationTarget == startPoint && !wasSwitched || wasSwitched && destinationTarget == endPoint)
        {
            departTarget = startPoint;
            destinationTarget = endPoint;
            wasSwitched = false;
        }
        else
        {
            departTarget = endPoint;
            destinationTarget = startPoint;
            wasSwitched = false;
        }
    }
    IEnumerator changeDelay()
    {
        yield return new WaitForSeconds(changeDirectionDelay);
        ChangeDestination();
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == Player)
        {
            Player.transform.parent = null;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == Player)
        {
            Player.transform.parent = transform;
        }
    }

}
