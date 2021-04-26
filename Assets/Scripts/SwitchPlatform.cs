using UnityEngine;
using UnityEngine.UI;

public class SwitchPlatform : MonoBehaviour
{
    public GameObject Platform;
    public GameObject Player;
    public Transform point; 

    [SerializeField] private bool isInsideTrigger = false;


    void Update()
    {
        if (isInsideTrigger)
        {
            if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Mouse0))
            {
                MovePlatform.givenSign = true;
            }
        }  
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == Player)
        {
            Debug.Log("Wcisnij E lub Mysz 0");
            isInsideTrigger = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == Player)
        {
            isInsideTrigger = false;
        }
    }
}
