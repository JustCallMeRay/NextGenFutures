/* 
public array strings
populate from inspector
print array.length
loop and print elements

extra 
use list
remove item when clicked
print size of list

*/ //Why is a '*' recomended for each line when it works fine without it?

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task1 : MonoBehaviour
{

    public int type = 0;
 
    public string[] Arr ;
   
    public List<string> myList = new List<string>();

    // Start is called before the first frame update
    void Start()
    {
		switch (type) //I'll use enums eventually
		{
		    case 2:
                Debug.Log(Arr.Length);
                for(int i = 0; i<Arr.Length; i++)
		        {
                    Debug.Log(Arr[i]);
		        }
                break;
            case 1:
                Debug.Log(myList.Count);
                for (int i = 0; i < myList.Count; i++)
				{
                    Debug.Log(myList[i]);
				}
                break;

		}

    }


    private void OnMouseDown() //My OnMouseDown doesn't work, I tried to fix it a couple days ago but gave up 
    {
        switch (type)
        {
            case 1:
                if (myList.Count > 1)
                {
                    myList.RemoveAt(myList.Count);
                    Debug.Log(myList.Count);
                }
                break;

            case 2:
                // Cannot do this for arr as fixed size 
                break;
        }

    }
}
