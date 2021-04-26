using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloorPrinter : MonoBehaviour
{
    public Text floorPrinterText;
    private GameObject playerObj = null;
    private int currentFloor;
    void Start()
    {
        if (playerObj == null)
            playerObj = GameObject.Find("Player");
        
    }

    void Update()
    {
        currentFloor = (int)playerObj.transform.position.y;
        floorPrinterText.text = "Floor: " + currentFloor / 6;
    }
}
