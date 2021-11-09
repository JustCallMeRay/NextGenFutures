using UnityEngine;

public class Room1WallText : MonoBehaviour
{
    public TextMesh deltaTimeOutput;
    public TextMesh fixedDeltaTimeOutput;
    public TextMesh unscaledDeltaTimeOutput;
    public TextMesh frameCountOutput;
    public TextMesh fps;
    public TextMesh Currentfps;
    int framecountCurrent; 
    int framecountLast = 0; 

    void Update()
    {
        framecountCurrent = Time.frameCount;
        int deltaframecount = framecountCurrent - framecountLast;
        deltaTimeOutput.text = Time.deltaTime.ToString();
        fixedDeltaTimeOutput.text = Time.fixedDeltaTime.ToString();
        unscaledDeltaTimeOutput.text = Time.unscaledDeltaTime.ToString();
        
        //*I wasn't thinking :/   */
        frameCountOutput.text = Time.frameCount.ToString();
        Currentfps.text = (deltaframecount/Time.unscaledDeltaTime).ToString(); //(deltaframecount/Time.deltaTime).ToString();
        fps.text = (Time.frameCount/ Time.time).ToString();
        framecountLast = Time.frameCount;
       
    }

    private void OnTriggerEnter(Collider other)
    {
	    if (other.CompareTag("Player"))
	    {
		    Time.timeScale = 0.5f;
	    }
    }								//Why did you give two scripts to fill?

    private void OnTriggerExit(Collider other)
    {
	    if (other.CompareTag("Player"))
	    {
		    Time.timeScale = 1;
	    }
    }

}
