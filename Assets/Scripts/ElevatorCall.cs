using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorCall : MonoBehaviour
{
    [SerializeField] private GameObject Player;

    private Transform Floor;

    [SerializeField] private bool isInsideTrigger = false;
    void Start()
    {
        

    }
    void Update()
    {
        if (isInsideTrigger)
        {
            if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Mouse0))
            {
                Floor = gameObject.transform.GetChild(0);
                Debug.Log(LiftControler.called = (int)(Floor.position.y / 10)+1);
            }
        }
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
            Debug.Log("Wcisnij E lub Mysz 0");
            Player.transform.parent = transform;
            isInsideTrigger = true;
        }
    }

}
