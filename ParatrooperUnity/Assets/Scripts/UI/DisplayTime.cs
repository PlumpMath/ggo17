using UnityEngine;
using UnityEngine.UI;

public class DisplayTime : MonoBehaviour
{

	[SerializeField]
	private BunkerManager bunkerManager;

	[SerializeField]
	private Text daysValue;
	
	[SerializeField]
	private Text minutesValue;
	
	[SerializeField]
	private Text secondsValue;

	[SerializeField]
	private Health bunker; 

	public string Minutes => Mathf.FloorToInt(this.secondsLeft / 60.0f) + "";
	public string Seconds
	{
		get
		{
			var seconds = Mathf.FloorToInt(this.secondsLeft % 60.0f);
			if(seconds < 10)
			{
				return "0" + seconds;
			}
			else
			{
				return "" + seconds;
			}
		}
	}

	private float secondsLeft;
	
	void Start ()
	{
		this.secondsLeft = this.bunkerManager.DayDurationSeconds;

		if(daysValue != null)
		{
			this.daysValue.text = this.bunkerManager.Day + " - ";
		}
	}
	
	void Update () {
		if(this.secondsLeft > 0.0f)
		{
			this.secondsLeft -= Time.deltaTime;
		}

		if(this.secondsLeft < this.bunkerManager.DayDurationSeconds * 0.1f)
		{
			// TODO: Stop spawning planes...
		}
		
		if(this.secondsLeft < 0.0f)
		{
			this.secondsLeft = 0.0f;
			
			this.bunkerManager.SurvivedDay(Mathf.FloorToInt(this.bunker.HitPoints)); // TODO: Remove this horrible hack!
		}

		if(this.minutesValue != null)
		{
			this.minutesValue.text = Minutes + ":";
		}

		if(this.secondsValue != null)
		{
			this.secondsValue.text = Seconds + "";
		}
	}
}
