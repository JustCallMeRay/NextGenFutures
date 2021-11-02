/*
Destroy door
*/

using UnityEngine;

public class BaseRoomButton : MonoBehaviour
{
    public GameObject door;
    public TextMesh output;

    bool pressed = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
			if (!pressed)
			{
            Destroy(door);
            output.text = "Well done!";
			}
        }
        pressed = true;
    }
}
