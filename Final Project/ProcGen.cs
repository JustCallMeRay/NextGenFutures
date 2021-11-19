using System;       //first time I've needed this 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class ProcGen
{
    public static ComputeShader _defaultComp = (ComputeShader)Resources.Load("Worley1");
    public static RenderTexture _defaultRT = new RenderTexture(_defaultRes.x, _defaultRes.y, 24);
    public static Vector2Int _defaultRes = new Vector2Int(256, 256);
    public static int[] Threadcount;     //unused
    private const uint BITNOISE1 = 0xB5297A4D;
    private const uint BITNOISE2 = 0x68E31DA4;
    private const uint BITNOISE3 = 0x1B56C4E9;
    private static uint RandCalled = 1;     //Increases randomness


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
        Vector4[] POINTS = null,
        uint resolutionx = 256,
        uint resolutiony = 256,
        uint Threadcountx = 8,
        uint Threadcounty = 8
        )
    {
        POINTS = (POINTS == null ? DebugPoints() : POINTS);
		ComputeShader ??= _defaultComp; //if null change
		renderTexture = new RenderTexture((int)resolutionx, (int)resolutiony, 24);
        renderTexture.enableRandomWrite = true;
        renderTexture.Create();


        ComputeShader.SetTexture(0, "Result", renderTexture);
        ComputeShader.SetFloats("res", renderTexture.width, renderTexture.height);
        ComputeShader.SetVectorArray("points", POINTS);

        ComputeShader.Dispatch(0,
              (renderTexture.width / (int)Threadcountx),
              (renderTexture.height / (int)Threadcounty), 1);    
    }

    public static void RefreshCompute(ComputeShader ComputeShader, RenderTexture renderTexture)
    {
        ComputeShader ??= _defaultComp; //if null change
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
    [Obsolete("Use a vector 2 or RandNoise1D")]
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
    //Overload 1
    public uint RandNoise(Vector2 pos, uint seed = 0)
    {
        //slightly more random if used as rand(), don't know if partial class resets vars?
        RandCalled++;
        pos.x = (int)((pos.x == int.MaxValue) ? RandCalled : (uint)pos.x);
        pos.y = (int)((pos.y == int.MaxValue) ? RandCalled : (uint)pos.y);
        seed = (seed == 0) ? RandCalled : seed;
        uint ret = (uint)pos.x*32;

        //Randomness 
        ret *= BITNOISE1;
        ret += seed;
        ret ^= (ret >> 8);
        ret += BITNOISE2;
        ret ^= (ret << 8);
        ret *= BITNOISE3;
        ret ^= (ret >> 8);
        ret += (uint)pos.y*256;
        return ret;
    }
    //Overload 2
    public uint RandNoise(Vector3 pos, uint seed = 0)
    { 
        //slightly more random if used as rand(), don't know if partial class resets vars?
        RandCalled++;
        pos.x = (int)((pos.x == int.MaxValue) ? RandCalled : (uint)pos.x);
        pos.y = (int)((pos.y == int.MaxValue) ? RandCalled : (uint)pos.y);
        seed = (seed == 0) ? RandCalled : seed;
        uint ret = (uint)(pos.x+1)*128;

        //Randomness 
        ret *= BITNOISE1;
        ret += seed;
        ret ^= (ret >> 8);
        ret += BITNOISE2;
        ret *= (uint)(pos.z + 2) * 1024;
        ret ^= (ret << 8);
        ret *= BITNOISE3;
        ret ^= (ret >> 8);
        ret += (uint)pos.y;
        return ret;
    }
    //Overload 3
    public uint RandNoise(Vector4 pos, uint seed = 0)
    {
        //slightly more random if used as rand(), don't know if partial class resets vars?
        RandCalled++;
        pos.x = (int)((pos.x == int.MaxValue) ? RandCalled : (uint)pos.x);
        pos.y = (int)((pos.y == int.MaxValue) ? RandCalled : (uint)pos.y);
        seed = (seed == 0) ? RandCalled : seed; //seed must be stated else gives more rand
        uint ret = (uint)pos.x*128;

        //Randomness 
        ret *= BITNOISE1;
        ret += seed;
        ret ^= (ret >> 8);
        ret += BITNOISE2;
        ret *= (uint)(pos.z + 2) * 1024;
        ret ^= (ret << 8);
        ret *= BITNOISE3;
        ret ^= (ret >> 8);
        ret += (uint)pos.y*512;
        return ret;
    }


    //Overload 0
    public double RandNoisef(Vector2 pos, uint seed = 0)
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
    //Overload 1
    public double RandNoisef(Vector3 pos, uint seed = 0)
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
    //Overload 2
    public double RandNoisef(Vector4 pos, uint seed = 0)
    {
        //slightly more random if used as rand(), don't know if partial class resets vars?
        RandCalled++;
        seed = (seed == 0) ? RandCalled : seed;
        uint reti = (uint)(pos.x + 2) * 1024;

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
    public double RandNoisefloat0to1(Vector2 pos, uint seed = 0)
    {
        return RandNoise(pos, seed) / uint.MaxValue;
    }
    //Overload 1
    public double RandNoisefloat0to1(Vector3 pos, uint seed = 0)
    {
        return RandNoise(pos, seed) / uint.MaxValue;
	}
    //Overload 2
    public double RandNoisefloat0to1(Vector4 pos, uint seed = 0)
    {
        return RandNoise(pos, seed) / uint.MaxValue;
    }
    //Overload 3
    public double RandNoisefloat0to1(int pos, uint seed = 0)
	{
        return RandNoise1D(pos, seed) / uint.MaxValue;
    }


    //Overload 0
    public double RandNoiseFNeg1to1(Vector2 pos, uint seed = 0)
    {
        return (RandNoise(pos, seed) / (uint.MaxValue / 2)) - 1;
    }
    //Overload 1
    public double RandNoiseFNeg1to1(Vector3 pos, uint seed = 0)
    {
		return (RandNoise(pos, seed) / (uint.MaxValue / 2)) - 1;
    }
    //Overload 2
    public double RandNoiseFNeg1to1(Vector4 pos, uint seed = 0)
    {
        return (RandNoise(pos, seed) / (uint.MaxValue / 2)) - 1;
    }
    //Overload 3 
    public double RandNoiseFNeg1to1(int pos, uint seed = 0)
    {
        return (RandNoise1D(pos, seed) / (int.MaxValue / 2)) - 1;
    }


    //Overload 0
    public bool RandNoiseBool(int pos, float bias = 0.5f)
    {
        RandCalled++;
        return (Mathf.Round((float)RandNoisefloat0to1(pos)+bias -0.5f) == 1);
	}
    //Overload 1
    public bool RandNoiseBool(Vector2 pos, float bias = 0.5f)
    {
        RandCalled++;
        return (Mathf.Round((float)RandNoisefloat0to1(pos) + bias - 0.5f) == 1);
    }
    //Overload 2
    public bool RandNoiseBool(Vector3 pos, float bias = 0.5f)
    {
        RandCalled++;
        return (Mathf.Round((float)RandNoisefloat0to1(pos) + bias - 0.5f) == 1);
    }
    //Overload 3
    public bool RandNoiseBool(Vector4 pos, float bias = 0.5f ){
        RandCalled++;
        return (Mathf.Round((float)RandNoisefloat0to1(pos) + bias - 0.5f) == 1);
    }
    
}
