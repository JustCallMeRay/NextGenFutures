using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DrawComputeShader : MonoBehaviour  
{
    public ComputeShader computeShader0;
    public ComputeShader computeShader1;
    public RenderTexture renderTexture;


    void Start()
    {
        renderTexture = new RenderTexture(256, 256, 24);
        renderTexture.enableRandomWrite = true;
        ProcGen.DebugComputeShader(renderTexture, computeShader0);

        GetComponent<Renderer>().material.SetTexture("_MainTex", renderTexture);
        Invoke("CallRefresh", 0.5f);
    }
    
	public void CallRefresh()
    {
        computeShader0.SetTexture(0, "Result", renderTexture);
        computeShader0.Dispatch(0, renderTexture.width / 8, renderTexture.height / 8, 1);
        GetComponent<Renderer>().material.SetTexture("_MainTex", renderTexture);
        Invoke("Comp2", 0.25f);
    }

    public void Comp2()
    {
        computeShader1.SetTexture(0, "Result", renderTexture);
        computeShader1.Dispatch(0, renderTexture.width / 8, renderTexture.height / 8, 1);
        GetComponent<Renderer>().material.SetTexture("_MainTex", renderTexture);
    }

}
