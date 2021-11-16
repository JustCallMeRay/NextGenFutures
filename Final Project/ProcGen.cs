using System;       //first time i've needed this XD
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class ProcGen
{
    public static ComputeShader DefaultComp; //get a ref to compute shader? 
    public static RenderTexture DefaultRT;      // and set as const 
    public static int[] Threadcount;     //unused
    private const uint BITNOISE1 = 0xB5297A4D;
    private const uint BITNOISE2 = 0x68E31DA4;
    private const uint BITNOISE3 = 0x1B56C4E9;
    private static uint RandCalled = 0;
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
        renderTexture = new RenderTexture(resolution[0], resolution[1], 24);
        renderTexture.enableRandomWrite = true;
        renderTexture.Create();


        ComputeShader.SetTexture(0, "Result", renderTexture);
        ComputeShader.SetFloats("res", renderTexture.width, renderTexture.height);
        ComputeShader.SetVectorArray("points", POINTS);

        ComputeShader.Dispatch(0,
            renderTexture.width / Threadcountx,
            renderTexture.height / Threadcounty, 1);
        new WaitForSeconds(0.5f);
        RefreshCompute(ComputeShader, renderTexture);
    }

    public static void RefreshCompute(ComputeShader ComputeShader, RenderTexture renderTexture)
    {
        ComputeShader.SetTexture(0, "Result", renderTexture);
        ComputeShader.Dispatch(0, renderTexture.width / 8, renderTexture.height / 8, 1);   
    }
   
    public static uint RandNoise1D(int pos = int.MaxValue, uint seed = 0 )
    {
        //slightly more random if used as rand(), don't know if partial class resets vars?
        RandCalled++;
        uint ret = (pos == int.MaxValue) ? RandCalled : (uint)pos;
        seed = (seed == 0) ? RandCalled : seed;

        //Randomness 
        ret *= BITNOISE1;
        ret += seed;
        ret ^= (ret >> 8);
        ret += BITNOISE2;
        ret ^= (ret << 8);
        ret *= BITNOISE3;
        ret ^= (ret >> 8);
        return ret; 
    }
    
    //Overload 0
    public uint RandNoise(int posx = int.MaxValue,int posy =int.MaxValue, uint seed = 0)
    {
        //slightly more random if used as rand(), don't know if partial class resets vars?
        RandCalled++;
        posx =(int) ((posx == int.MaxValue) ? RandCalled : (uint)posx);
        posy = (int) ((posy == int.MaxValue) ? RandCalled : (uint)posy);
        seed = (seed == 0) ? RandCalled : seed;
        uint ret =(uint) posx;
        
        //Randomness 
        ret *= BITNOISE1;
        ret += seed;
        ret ^= (ret >> 8);
        ret += BITNOISE2;
        ret ^= (ret << 8);
        ret *= BITNOISE3;
        ret ^= (ret >> 8);
        ret += (uint)posy;
        return ret;
    }

    //Overload 2
    public double RandNoise(Vector2 pos, uint seed = 0)
    {
        //slightly more random if used as rand(), don't know if partial class resets vars?
        RandCalled++;
        seed = (seed == 0) ? RandCalled : seed;
        uint reti = (uint)pos.x*1000;

        //Randomness 
        reti *= BITNOISE1;
        reti += seed;
        reti ^= (reti >> 8);
        reti += BITNOISE2;
        reti ^= (reti << 8);
        reti *= BITNOISE3;
        reti ^= (reti >> 8);
        double retf = (reti + Math.PI ) / 1000f; 
        reti += (uint)pos.y;
        return retf;
    }

    //Overload 3
    public double RandNoise(Vector3 pos, uint seed = 0)
    {
        //slightly more random if used as rand(), don't know if partial class resets vars?
        RandCalled++;
        seed = (seed == 0) ? RandCalled : seed;
        uint reti = (uint)(pos.x+2) * 1024;

        //Randomness 
        reti *= BITNOISE1;
        reti += seed;
        reti ^= (reti >> 8);
        reti += BITNOISE2;
        reti *= (uint)(pos.z + 2) * 1024;
        reti ^= (reti << 8);
        reti *= BITNOISE3;
        reti ^= (reti >> 8);
        double retf = (reti + Math.PI) / 1024f;
        reti += (uint)pos.y;
        return retf;
    }

    //Overload 0
    public bool RandNoiseBool(float bias = 0.5f)
    {
        RandCalled++;
        bool ret;
        if (bias > 0.5f)
        {
            ret = (RandNoise1D() % (1 / (1-bias)) == 1);
            ret = !ret;
        } else { 
            ret = (RandNoise1D() % (1 / bias) == 1);
        }
        return ret;
	}

    //Overload 1
    public bool RandNoiseBool(Vector2 pos, float bias = 0.5f)
    {
        RandCalled++;
        bool ret;
        if (bias > 0.5f)
        {
            ret = (RandNoise(pos) % (1 / (1 - bias)) == 1);
            ret = !ret;
        }
        else
        {
            ret = (RandNoise(pos) % (1 / bias) == 1);
        }
        return ret;
    }
    //Overload 2
    public bool RandNoiseBool(Vector3 pos, float bias = 0.5f)
    {
        RandCalled++;
        bool ret;
        if (bias > 0.5f)
        {
            ret = (RandNoise(pos) % (1 / (1 - bias)) == 1);
            ret = !ret;
        }
        else
        {
            ret = (RandNoise(pos) % (1 / bias) == 1);
        }
        return ret;
    }

    //float version (0-1) of RandNoise() 
    /* public static float RandNoiseFloat(int pos = int.MaxValue, uint seed = 0)
     {
         //slightly more random if used as rand()
         RandCalled++;
         uint ret = (pos == int.MaxValue) ? RandCalled : (uint)pos;
         seed = (seed == 0) ? RandCalled : seed;

         //Randomness 
         ret *= BITNOISE1;
         ret += seed;
         ret ^= (ret >> 8);
         ret += BITNOISE2;
         ret ^= (ret << 8);
         ret *= BITNOISE3;
         ret ^= (ret >> 8);
         return ret;
     }*/



}
