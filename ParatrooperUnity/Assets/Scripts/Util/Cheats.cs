using UnityEngine;

public class Cheats : MonoBehaviour
{

	public bool CheatsEnabled = true;
	public Transform LaneTop;
	public Transform LaneMiddle;
	
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
		else if (Input.GetKeyDown(KeyCode.Semicolon))
		{
			LaneMiddle.GetComponent<LaneManager>().SpawnLeft();
		}
		else if (Input.GetKeyDown(KeyCode.Quote))
		{
(((())))			LaneMiddle.GetComponent<LaneManager>().SpawnRight();
		}
	}
}
