using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class coin1 : MonoBehaviour
{
    [HideInInspector]
    public enum TransformMethod
    {  
        Onshader,       //Wrote a shader to do all the animation
        OnCPU
	}

    public TransformMethod TransMethod;
    private Material mat;
    public float Height = 1;
    public float Rot = 1;
    public float animLength = 1;
    bool AnimPlaying = false;
    private float T_Start;


    void Start()
    {
        mat = gameObject.GetComponent<Renderer>().material;
        InvokeRepeating("flipCoin", 0.25f, animLength + 1f);
    }

    public void flipCoin()
    {
        if (!AnimPlaying) 
        {
            if (TransMethod == TransformMethod.OnCPU)
            {
                playAnim();
            } else { //set shader anim length
            }
            Invoke("CalcRand", animLength / 2);
        }
    }
    private void CalcRand()
    {
        int rand = Mathf.RoundToInt(Random.Range(0f, 1f));
        mat.SetFloat("CoinSide", rand);
    }

    public void playAnim()
    {
        Invoke("Anim2false", animLength);
        AnimPlaying = true;
        transform.DORotate(new Vector3(1,0,0)*Rot*360,animLength,RotateMode.WorldAxisAdd);
        T_Start = Time.time;
      
    }

    private void Anim2false()
    {
        AnimPlaying = false;
        transform.DORotate(new Vector3(0, 0, 0), 0, RotateMode.FastBeyond360);
        //print("false");
    }

	private void Update()
	{
        if (TransMethod == TransformMethod.OnCPU)
        {
            float t = Time.time - T_Start;
            float T = (Mathf.Sin(((Time.time - T_Start) * Mathf.PI) / animLength)); 
            if (t > 0 && t < animLength + 0.1f) // little fiddle
            {
                transform.position = Vector3.Lerp(
                    new Vector3(0, 0, 0),
                    new Vector3(0, Height, 0),
                    T);
            }
        }
	}
}
