using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mycoin : MonoBehaviour
{
    public Animation CoinFlipAnim;

    public bool flipCoin(){
        CoinFlipAnim.Play();
        return ((Mathf.RoundToInt(Random.value) == 1));
	}
    


}
