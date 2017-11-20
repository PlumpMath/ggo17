using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class DisplayTroopersKilled : MonoBehaviour {

	private Text text;
	
	void Awake()
	{
		this.text = GetComponent<Text>();
	}

	void Update () {
		text.text = PointsManager.Instance.TroopersKilled + "";
	}
}
