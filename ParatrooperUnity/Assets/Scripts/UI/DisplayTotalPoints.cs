using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class DisplayTotalPoints : MonoBehaviour {

	private Text totalPointsText;

	void Awake()
	{
		this.totalPointsText = this.GetComponent<Text>();
	}

	void Update () {
		this.totalPointsText.text = PointsManager.Instance.TotalPoints + "";
	}
}
