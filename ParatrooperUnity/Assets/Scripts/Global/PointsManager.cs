using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;

public class PointsManager : MonoBehaviour
{

	public static PointsManager Instance = null;

	public int Points
	{
		get
		{
			return this.points;
		}
		set
		{
			this.points = value;
		}
	}

	public int TotalPoints => this.totalPoints;
	public int TroopersKilled => this.troopersKilled;
	public int TroopersFell => this.troopersFell;
	public int TransportsKilled => this.transportKills;
	
	[SerializeField]
	[Tooltip("Current points available to spend.")]
	private int points;

	[SerializeField]
	[Tooltip("Points for all time (including spent points).")]
	private int totalPoints;
	
	[SerializeField]
	[Tooltip("Number of troopers killed.")]
	private int troopersKilled;
	
	[SerializeField]
	[Tooltip("Number of troopers that fell to their deaths.")]
	private int troopersFell;
	
	[SerializeField]
	[Tooltip("Number of transports killed.")]
	private int transportKills;

	public void AddPoints(int points)
	{
		this.points += points;
		this.totalPoints += points;
	}

	public void DeductPoints(int points)
	{
		this.points -= points;

		if (this.points < 0)
		{
			this.points = 0;
		}
	}

	public void AddTrooperKill()
	{
		this.troopersKilled++;
	}

	public void AddTrooperFell()
	{
		this.troopersFell++;
	}

	public void AddTransportKill()
	{
		this.transportKills++;
	}

	private void Awake()
	{
		if(Instance != null && Instance != this)
		{
			Destroy(this.gameObject);
		}
		else
		{
			DontDestroyOnLoad(this.gameObject);
			Instance = this;			
		}
	}

// Example of simple disk serialization
//	public void Save()
//	{
//		Data data = new Data();
//		data.test = "Hello world!";
//		
//		BinaryFormatter binaryFormatter = new BinaryFormatter();
//		FileStream file = File.Open(Application.persistentDataPath + "/slot1.sav", FileMode.Open);
//		binaryFormatter.Serialize(file, data);
//		file.Close();
//	}
}

// Example of simple disk serialization
//[Serializable]
//class Data
//{
//	public String test;
//	
//}