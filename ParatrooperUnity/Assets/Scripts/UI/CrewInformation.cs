using UnityEngine;
using UnityEngine.UI;

public class CrewInformation : MonoBehaviour
{

	[SerializeField]
	private CrewManager crewManager;
	[SerializeField]
	private Text crewCount;
	[SerializeField]
	private HealthBar crewHealth;
	
	void Update ()
	{
		crewCount.text = crewManager.CrewCount + "";
		crewHealth.MaxHealth = crewManager.MaxHitPoints;
		crewHealth.Health = crewManager.HitPoints;
	}
}
