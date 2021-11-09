/************   You didn't give us much time so this isn't very good ********/ 


using UnityEngine;

public class TranslateCube : MonoBehaviour
{
	private void Update()
	{
		float x = 3 * Time.deltaTime;
		gameObject.transform.position += new Vector3(x, 0, 0);
	}
	// Over to you!
}

using UnityEngine;

public class LerpCube : MonoBehaviour
{
    public Transform target;
	public Vector3 startposition;
	public Vector3 endposition;
	// Over to you!

	private void Start()
	{
		startposition = gameObject.transform.position;
		endposition = target.position;
	}

	private void Update()
	{
		Vector3.Lerp(startposition, endposition, Time.time);	
		
		/*		oops :(		
		float x = Mathf.Lerp(startposition.x, endposition.x, Time.time);
		float y = Mathf.Lerp(startposition.y, endposition.y, Time.time);
		float z = Mathf.Lerp(startposition.z, endposition.z, Time.time);
		gameObject.transform.position = new Vector3(x, y, z);
		*/
		
	}
}

using UnityEngine;

public class MoveTowardsCube : MonoBehaviour
{
    public Transform target;
	public Vector3 startposition;
	public Vector3 endposition;
	// Over to you!

	private void Start()
	{
		startposition = gameObject.transform.position;
		endposition = target.position;
	}

	private void Update()
	{
		transform.position = Vector3.MoveTowards(
			gameObject.transform.position, 
			endposition, 
			Time.fixedDeltaTime);
	}
}


