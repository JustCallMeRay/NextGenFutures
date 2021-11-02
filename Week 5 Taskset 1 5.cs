/*
start of Proc-gen explination, 
not really a task. 
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessingAbout : MonoBehaviour //Direct quote. 
{
    void Start()
    {
        var mesh = GetComponent<MeshFilter>().mesh;

        Vector3[] verts = mesh.vertices;

        verts[1].y++; //? 

        mesh.vertices = verts;


        for (int i = 0; i < verts.Length; i++)
	{
		if(verts[i].y > 0)
	      	{
                 verts[i].y++; 
		}
	}
    }

 
}
