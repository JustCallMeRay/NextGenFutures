// This code wasn't debuged and is now
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugScript : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
		{
            Debug.Log("You have pressed 'A'");

		}
        if (Input.GetKeyDown(KeyCode.Space))
		{
            Debug.Log("You have pressed Space Bar");
		}
    }
}
