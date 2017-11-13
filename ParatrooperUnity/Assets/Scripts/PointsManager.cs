using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class PointsManager : MonoBehaviour {

	public static int Points { get; private set; }

	private Text pointsText;  
	
	public static void AddPoints(int points)
	{
		Points += points;
	}

	public static void DeductPoints(int points)
	{
		Points -= points;

		if (Points < 0)
		{
			Points = 0;
		}
	}

	private void Awake()
	{
		pointsText = GetComponent<Text>();
	}
	
	void Update ()
	{
		pointsText.text = Points + "";
	}
}
