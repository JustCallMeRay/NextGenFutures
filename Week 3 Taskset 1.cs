using UnityEngine;

public class CubeScript : MonoBehaviour //I edited the main script instead of creating a new one which is why I lost it :(
{
    private int rotationAmount = 20;
    public int instType = 0;

    void OnMouseDown()
    {
		switch (instType)
		{
            case 1:
                transform.Rotate(0, rotationAmount, 0);
                break;
            case 2:
                transform.Rotate(0, rotationAmount, 0);
                transform.Translate(0, 2, 0); //I had this as a force thing but i lost it  ¯\_('_')_/¯
                break;
            case 3:
                transform.Rotate(0, rotationAmount, 0);
                break;
            default: print("(user) error"); break;

        }
       
    }
}
