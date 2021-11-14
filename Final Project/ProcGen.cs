using System;       //first time i've needed this XD
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class ProcGen
{
    public static ComputeShader DefaultComp; //get a ref to compute shader? 
    public static RenderTexture DefaultRT;
    public static int[] Threadcount; 

    public static Vector4[] DebugPoints()
    {
            return new Vector4[]      //~~float2 on shader, float 4 on cpu?~~
            {                               //now float4 on compute

            new Vector4( 0, 0,               0, 0 ),
            new Vector4( 0.8f, 0.4f,         1, 1 ),
            new Vector4( 0.5f, 0.5f,         0.5f, 1 ),
            new Vector4(0, 1,                1, 1 ),
            new Vector4(1, 0,                1, 1 ),
            new Vector4(0.2f, 0.6f,          1, 1 ),
            new Vector4(10, 10,              1, 1 ),
            new Vector4( 10, 10,             1, 1),
            new Vector4(10, 10,              1, 1 )
            };
    }
   
    [Obsolete("Use vec4 points, not floats")]
    public static float[,] DebugPoints(float Type)
    {
       
            return new float[9, 4]      //float2 on shader, float 4 on cpu?
        {  
            { 0, 0, 0, 0 },
            { 1, 1, 1, 1 },
            { 0.5f, 0.5f, 0.5f, 1 },
            { 0, 1, 1, 1 },
            { 1, 0, 1, 1 },
            { 0.2f, 0.6f, 1, 1 },
            { 10, 10, 1, 1 },
            { 10, 10, 1, 1 },
            { 10, 10, 1, 1 }
        };
        
    }
        
    


    public static void DebugComputeShader(
        RenderTexture renderTexture,
        ComputeShader ComputeShader,
        Vector4[] POINTS =null,
        int resolutionx = 256,
        int resolutiony = 256,
        int Threadcountx = 8,
        int Threadcounty = 8
        )
    {
        POINTS = (POINTS == null ? DebugPoints() : POINTS);
        //ComputeShader = (ComputeShader == null ? defRef : ComputeShader);
        int[] resolution = { resolutionx, resolutiony };
        //renderTexture = new RenderTexture(resolution[0], resolution[1], 24);
        renderTexture.enableRandomWrite = true;
        renderTexture.Create();


        ComputeShader.SetTexture(0, "Result", renderTexture);
        ComputeShader.SetFloats("res", renderTexture.width, renderTexture.height);
        ComputeShader.SetVectorArray("points", POINTS);

        ComputeShader.Dispatch(0,
            renderTexture.width / Threadcountx,
            renderTexture.height / Threadcounty, 1);
        new WaitForSeconds(0.5f);
        CallRefresh(ComputeShader, renderTexture);
    }
    

    [Obsolete("Use vec4 points, not floats")]
    public static void DebugComputeShader(
        RenderTexture renderTexture,
        ComputeShader ComputeShader,
        float type,
        float[,] POINTS = null,
        int resolutionx = 256,
        int resolutiony = 256,
        int Threadcountx = 8,
        int Threadcounty = 8
        )
    {
        POINTS = (POINTS == null ? DebugPoints(0f) : POINTS);
        //ComputeShader = (ComputeShader == null ? defRef : ComputeShader);
        int[] resolution = { resolutionx, resolutiony }; 
        //renderTexture = new RenderTexture(resolution[0], resolution[1], 24);    //3d texture?
        renderTexture.enableRandomWrite = true;
        renderTexture.Create();


        ComputeShader.SetTexture(0, "Result", renderTexture);
        ComputeShader.SetFloats("res", renderTexture.width, renderTexture.height);
        ComputeShader.SetFloats("points",
        POINTS[0, 0], POINTS[0, 1], //POINTS[0, 2], POINTS[0, 3],
        POINTS[1, 0], POINTS[1, 1], //POINTS[1, 2], POINTS[1, 3],
        POINTS[2, 0], POINTS[2, 1], //POINTS[2, 2], POINTS[2, 3],
        POINTS[3, 0], POINTS[3, 1], //POINTS[3, 2], POINTS[3, 3],
        POINTS[4, 0], POINTS[4, 1], //POINTS[4, 2], POINTS[4, 3],
        POINTS[5, 0], POINTS[5, 1], //POINTS[5, 2], POINTS[5, 3],
        POINTS[6, 0], POINTS[6, 1], //POINTS[6, 2], POINTS[6, 3],
        POINTS[7, 0], POINTS[7, 1], //POINTS[7, 2], POINTS[7, 3],
        POINTS[8, 0], POINTS[8, 1]//, POINTS[8, 2], POINTS[8, 3]
        );                    // I would like to nominate this as the worst code ever

        ComputeShader.Dispatch(0, 
            renderTexture.width / Threadcountx,
            renderTexture.height / Threadcounty, 1);
       new WaitForSeconds(0.5f);
       CallRefresh(ComputeShader,renderTexture);
    }




    public static void CallRefresh(ComputeShader ComputeShader, RenderTexture renderTexture)
    {
        ComputeShader.SetTexture(0, "Result", renderTexture);
        ComputeShader.Dispatch(0, renderTexture.width / 8, renderTexture.height / 8, 1);   
    }
   
    
}
