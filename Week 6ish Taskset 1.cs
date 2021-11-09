using UnityEngine;

public class BaseRoomButton : MonoBehaviour
{
	public TextMesh output;

	private void Update()
	{
		output.text = (Time.time.ToString() + " "  + Time.timeScale.ToString());
		
	}	// can I do new line in a textmesh, tried /n, output.text.newline ect. 


	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			Time.timeScale = 0.5f;
		}
		
	}

	private void OnTriggerExit(Collider other)
	{
		if(other.CompareTag("Player"))
		{
			Time.timeScale = 1;
		}
	}

}

