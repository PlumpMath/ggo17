using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class DisplayPoints : MonoBehaviour
{

	private Text pointsText;
	
	void Awake()
	{
		pointsText = GetComponent<Text>();
	}

	void Update () {
		pointsText.text = PointsManager.Instance.Points + "";
	}
}
