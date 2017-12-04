using Lanes;
using UnityEngine;

public class Cheats : MonoBehaviour
{

	public bool CheatsEnabled = true;
	public LaneManager LaneTop;
	public LaneManager LaneMiddle;
	public CrewManager CrewManager;
	public DisplayTime displayTime;

	void Awake()
	{
		this.CheatsEnabled = Application.isEditor;
	}
	
	void Update ()
	{
		if (!CheatsEnabled) return;
		
		LaneTopCheats();
		LaneMiddleCheats();

		GuardLeftCheats();
		GuardRightCheats();

		PauseTimer();
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
			CrewManager.AddGuardLeft();
		}
	}
	
	private void GuardRightCheats()
	{
		if (Input.GetKeyDown(KeyCode.PageDown))
		{
			CrewManager.AddGuardRight();
		}
	}
	
	private void PauseTimer()
	{
		if(Input.GetKeyDown(KeyCode.Pause))
		{
			this.displayTime.TogglePause();
		}
	}
}
