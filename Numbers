using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Numbers : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    void notMainInt()
    {
        int Myint = 40;
        //print(Myint);
        //print(Myint * 10);
        //  Myint *= 10 //you want this?
        int NotMyint = 60;
        //print(Myint + NotMyint);

        int Third = Myint + NotMyint;
        //print(Third);

        gameObject.transform.eulerAngles += new Vector3(0, Third, 0);
    }

    void notMainFloat()
    {
        float Myfloat = 4.5f;
        print(Myfloat);
        print(Myfloat * 10);
        //  Myfloat *= 10 ? 
        float NotMyfloat = 6.5f;
        print(Myfloat + NotMyfloat);

        float Third = Myfloat + NotMyfloat;
        print(Third);

        gameObject.transform.eulerAngles = new Vector3(0, Third, 0);
    }

    // Update is called once per frame
    void Update()
    {
        notMainInt();
    }
}
