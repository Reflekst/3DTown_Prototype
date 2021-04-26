using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JumpCounter : MonoBehaviour
{
    public Text jumpCounterText;

    void Update()
    {
        jumpCounterText.text = "Jumps: " + PlayerControler.jumps;
    }
}
