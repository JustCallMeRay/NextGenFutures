using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DrawComputeShader : MonoBehaviour  //should be class
{
    public ComputeShader computeShader;
    public RenderTexture renderTexture;


    void Start()
    {
        renderTexture = new RenderTexture(256, 256, 24);
        renderTexture.enableRandomWrite = true;
        ProcGen.DebugComputeShader(renderTexture, computeShader);

        GetComponent<Renderer>().material.SetTexture("_MainTex", renderTexture);
        Invoke("CallRefresh", 0.5f);
    }
    
	public void CallRefresh()
    {
        computeShader.SetTexture(0, "Result", renderTexture);
        computeShader.Dispatch(0, renderTexture.width / 8, renderTexture.height / 8, 1);
        GetComponent<Renderer>().material.SetTexture("_MainTex", renderTexture);
    }
    
}
