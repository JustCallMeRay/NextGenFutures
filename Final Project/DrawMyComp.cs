using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class DrawMyComp : MonoBehaviour
{
    public ComputeShader computeShader;
    
    public RenderTexture renderTexture;
	public int[] resolution = new int[2] { 256, 256 };
    public int item;
    public Vector4[] points =
    {
            new Vector4( 0, 0,               0, 0 ),
            new Vector4( 0.8f, 0.4f,         1, 0 ),
            new Vector4( 0.5f, 0.5f,         0.5f, 1 ),
            new Vector4(0, 1,                0, 0 ),
            new Vector4(1, 0,                0, 0 ),
            new Vector4(0.2f, 0.6f,          0, 0 ),
            new Vector4(10, 10,              0, 0 ),
            new Vector4( 10, 10,             0, 0),
            new Vector4(10, 10,              0, 0 )
    };

    private void Start()
	{
        //renderTexture = new RenderTexture(resolution[0], resolution[1], 24);    //3d texture?
        ProcGen.DebugComputeShader(renderTexture, computeShader, points, item);
        GetComponent<Renderer>().material.SetTexture("_MainTex", renderTexture);
        InvokeRepeating("CallRefresh", 0.5f,0.5f);    //Doesn't work first time
    }

	public void CallRefresh()
    {
        //computeShader.SetTexture(0, "Result", renderTexture);
       // computeShader.Dispatch(0, renderTexture.width / 8, renderTexture.height / 8, 1);
        ProcGen.RefreshCompute(computeShader, renderTexture);  //dont run me 
        GetComponent<Renderer>().material.SetTexture("_MainTex", renderTexture);
        computeShader.SetVectorArray("points",points);   
        //I can't have a vector in hlsl but they still call it a vec ::angry::
        
        computeShader.SetFloats("res", 256, 256, item);
    }
	
}
