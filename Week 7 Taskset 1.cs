using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mycoin : MonoBehaviour
{
    public Animation CoinFlipAnim;
    public Material material;
    public bool flipCoin(){
        CoinFlipAnim.Play();
        material.SetInteger("Face", 1);
        return ((Mathf.RoundToInt(Random.value) == 1));
	}
}
