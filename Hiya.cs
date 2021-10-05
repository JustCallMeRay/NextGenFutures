using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hiya /*Class name must be the same as file name*/  : MonoBehaviour /* base class on "MonoBehaviour" class*/
{
    // Start is called before the first frame update
    void Start()
    {
        //Task 1 and 2 on powerpoint slides
		print("print string");
        NotMain();
        
    }

    void NotMain()
	{
        Debug.Log("Hello World");

        string playerName = "Call me Ray";

        print("Hello " + playerName + " it is very nice to meet you");
        print($"I am not {playerName} but thats okay");

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown()
	{
        Destroy(gameObject, 1.2f);
	}

    void OnDestroy()
	{
        print("aghhh, curse you fellow human");
	}

}
