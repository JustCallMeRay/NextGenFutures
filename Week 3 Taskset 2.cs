/*
Brief:
1 Store int clicks in class
2 Clicks ++ on click
3 Destroy gameobject when clicks > 10

Extra 
1 Rotate cube based on clicks
2 Scale cube based on clicks 
3 Record time on 1st and 10th click
4 log time for 10 clicks
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Taskset2: MonoBehaviour
{
    // Start is called before the first frame update
    public int Clicks = 0;
    float mytime = 0f;
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	private void OnMouseDown()
	{
        if(Clicks == 1)
		{
            print("first click");
            mytime = Time.time;
		}
        else
		{
            if(Clicks == 10)
			{
                print("10th click");
                Object.Destroy(gameObject);
                print(Time.time - mytime);
			}
		}
        
        Clicks++;

    }
}
