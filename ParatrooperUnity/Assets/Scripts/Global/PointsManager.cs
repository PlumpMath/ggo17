using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
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

	public void Save()
	{
		Data data = new Data();
		data.test = "Hello world!";
		
		BinaryFormatter binaryFormatter = new BinaryFormatter();
		FileStream file = File.Open(Application.persistentDataPath + "/slot1.sav", FileMode.Open);
		binaryFormatter.Serialize(file, data);
		file.Close();
	}
}

[Serializable]
class Data
{
	public String test;
	
}