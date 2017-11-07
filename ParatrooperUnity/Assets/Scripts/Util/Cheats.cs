using UnityEngine;

public class Cheats : MonoBehaviour
{

	public bool CheatsEnabled = true;
	public Transform LaneTop;
	
	void Update ()
	{
		if (!CheatsEnabled) return;
		
		if (Input.GetKeyDown(KeyCode.LeftBracket))
		{
			LaneTop.GetComponent<LaneManager>().SpawnLeft();
		}
		else if (Input.GetKeyDown(KeyCode.RightBracket))
		{
			LaneTop.GetComponent<LaneManager>().SpawnRight();
		}
	}
}
