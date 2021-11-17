using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class DrawMyComp : MonoBehaviour
{
    public ComputeShader computeShader;
    
    public RenderTexture renderTexture;
	public Vector2Int resolution = new Vector2Int( 256, 256 ); //unused
    public int item;
    public Vector4[] points =       //Draw gizmos for each point
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
        renderTexture = new RenderTexture(resolution.x, resolution.y, 24); //24 is bit depth
        renderTexture.enableRandomWrite = true;
        ProcGen.DebugComputeShader(renderTexture, computeShader, points);

        GetComponent<Renderer>().material.SetTexture("_MainTex", renderTexture);
        InvokeRepeating("CallRefresh", 0.5f,0.7f);
    }

	public void CallRefresh()
    {
        //computeShader.SetTexture(0, "Result", renderTexture);
       // computeShader.Dispatch(0, renderTexture.width / 8, renderTexture.height / 8, 1);
        ProcGen.RefreshCompute(computeShader, renderTexture);  
        GetComponent<Renderer>().material.SetTexture("_MainTex", renderTexture);
        computeShader.SetVectorArray("points",points);   
        //I can't have a vector in hlsl but they still call it a vec ::angry::
        
        computeShader.SetFloats("res", 256, 256, item);
        print("called callrefresh");
    }
    
}
