using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debugmatrix : MonoBehaviour
{

    public Vector2 chunkSize = new Vector2(1, 1);
    public Vector3 chunkCenter = new Vector3(0.5f, 0, 0.5f);

    [SerializeField]
    private Vector3 preTransform;
    private Vector3 postTransform;

    private Vector2 min;

    [SerializeField]
    private Matrix4x4 pointTransform = new Matrix4x4();//cannot be simplifed to new()
    //this looks awfull, why not serialize it like a vec4[]?
    

	private void OnDrawGizmosSelected()
	{
        min = new Vector2(chunkCenter.x, chunkCenter.z) - (chunkSize / 2);
            
        //      matrix scaling      This would look better in z up! Just saying
        /*x*/pointTransform[0, 0] = 1 / chunkSize.x;
        /*y*/pointTransform[1, 1] = 1; //make sure its not 0 ( p.y*1 => p )
        /*z*/pointTransform[2, 2] = 1 / chunkSize.y;
        /*w*/pointTransform[3, 3] = 1; //Must be 1 in a tranform (idk, maths) 
        
           // matrix translation    (movement)
        /*x*/pointTransform[0, 3] = (min.x);
        /*y*/pointTransform[1, 3] = 0;      // p.y + 0 => p.y   // -min.z;
        /*z*/pointTransform[2, 3] = (min.y); 
        //      w      already set       (3,3);
    

        //      vector maths
        
            //CSpoints[i] = pointTransform.MultiplyPoint(WS_points[i]);
            postTransform = pointTransform.MultiplyPoint(preTransform); //3x4 is faster?
            //print(i);
       

        Gizmos.color = Color.black;
        Gizmos.DrawCube(new Vector3(0.5f, 0, 0.5f), new Vector3(1, 0, 1));
        
        Gizmos.color = Color.cyan;
        Gizmos.DrawCube(chunkCenter, new Vector3(chunkSize.x, 0, chunkSize.y));
        Gizmos.DrawSphere(chunkCenter, 0.05f);

                    //Change this^ to pre and post transform

		
        
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(preTransform, 0.1f);
		
        
        
        
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(postTransform, 0.1f);
		
	}
}
