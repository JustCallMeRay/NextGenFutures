/* using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mycoin : MonoBehaviour
{
    public Animation CoinFlipAnim;
    public Material material;
    public bool flipCoin()
  	{
        CoinFlipAnim.Play();
        material.SetInteger("Face", 1);
        return ((Mathf.RoundToInt(Random.value) == 1));
	}
}
*/ 

//AFTER DEBUGING (thanks Lukus and Ant)

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mycoin : MonoBehaviour
{
    public Animation CoinFlipAnim;
    public Material material;
    public bool flipCoin()
    {
        CoinFlipAnim.Play();
        int rand = Mathf.RoundToInt(Random.value);
        material.SetInteger("Face", rand);	// anim shopuld take longer than a drawcall, else weirdness 
        return (rand == 1);				//doesn't have a .SetBool
	}						// Didn't have time to write a hlsl shader and urp wasn't in the project 
}
