using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandcapeMeshGen : MonoBehaviour
{
    public Vector2 chunkSize = new Vector2(1, 1);
    public Vector3 chunkCenter = new Vector3(0.5f, 0, 0.5f);


    [SerializeField]
    private Matrix4x4 pointTransform = new Matrix4x4();//cannot be simplifed to new()
    //this looks awfull, why not serialize it like a vec4[]?
    
    
    //matrix turns points into relvent points for computeshader
       //so bounds when => 0-1, then points => points transformed

    private Vector2 min;

    [SerializeField]
    private Vector3[] WS_points = new Vector3[9];   //Points in world space "_" for unity
    [SerializeField] //for debuging, unneeded so removed for performance.
    private Vector3[] CSpoints = new Vector3[9]; //points in "computeshader space"

    
	public Vector3[] WS_Points => WS_points;  //A poor man's readonly
	public Vector3[] CS_Points => CSpoints;   //A poor man's readonly



	private void OnDrawGizmosSelected()
	{
        min = new Vector2(chunkCenter.x, chunkCenter.z) - (chunkSize / 2);
            // matrix scaling   This would look better in z up!
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
        for (int i=0; i<9 ; i++)
        {
            //CSpoints[i] = pointTransform.MultiplyPoint(WS_points[i]);
            CSpoints[i] = pointTransform.MultiplyPoint(WS_points[i]); //faster
            //print(i);
        }

        Gizmos.color = Color.black;
        Gizmos.DrawCube(new Vector3(0.5f, 0, 0.5f), new Vector3(1, 0, 1));
        
        Gizmos.color = Color.cyan;
        Gizmos.DrawCube(chunkCenter, new Vector3(chunkSize.x, 0, chunkSize.y));
        Gizmos.DrawSphere(chunkCenter, 0.05f);

		foreach (Vector3 p in WS_points)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(p, 0.1f);
		}
        
        foreach (Vector3 p in CSpoints)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(p, 0.1f);
		}
	}
}
