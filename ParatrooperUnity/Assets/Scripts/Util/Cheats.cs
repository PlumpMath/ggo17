using UnityEngine;

public class Cheats : MonoBehaviour
{

	public bool CheatsEnabled = true;
	public LaneManager LaneTop;
	public LaneManager LaneMiddle;
	public Guard GuardLeft;
	public Guard GuardRight;
	
	void Update ()
	{
		if (!CheatsEnabled) return;
		
		LaneTopCheats();
		LaneMiddleCheats();

		GuardLeftCheats();
		GuardRightCheats();
	}

	private void LaneTopCheats()
	{
		if (Input.GetKeyDown(KeyCode.LeftBracket))
		{
			LaneTop.SpawnLeft();
		}
		else if (Input.GetKeyDown(KeyCode.RightBracket))
		{
			LaneTop.SpawnRight();
		}
	}
	
	private void LaneMiddleCheats()
	{
		if (Input.GetKeyDown(KeyCode.Semicolon))
		{
			LaneMiddle.SpawnLeft();
		}
		else if (Input.GetKeyDown(KeyCode.Quote))
		{
			LaneMiddle.SpawnRight();
		}
	}
	
	private void GuardLeftCheats()
	{
		if (Input.GetKeyDown(KeyCode.Delete))
		{
			GuardLeft.ToggleCover();
		}
	}
	
	private void GuardRightCheats()
	{
		if (Input.GetKeyDown(KeyCode.PageDown))
		{
			GuardRight.ToggleCover();
		}
	}
}
