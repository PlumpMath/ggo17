using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using UnityEngine;
using UnityEngine.Analytics;

public class AnalyticsGameOver : MonoBehaviour
{

	[SerializeField]
	private BunkerManager bunkerManager;

	void Awake()
	{
		Analytics.CustomEvent("gameOver", new Dictionary<string, object>
		  {
			  { "level", "Day " + this.bunkerManager.Day},
			  { "score", PointsManager.Instance.TotalPoints},
			  { "transportKills", PointsManager.Instance.TransportsKilled},
			  { "trooperKills", PointsManager.Instance.TroopersKilled},
			  { "trooperFalls", PointsManager.Instance.TroopersFell},
		  });

	}
}
