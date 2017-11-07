using System.Collections.Generic;
using UnityEngine;

public class LaneManager : MonoBehaviour
{

	public Transform TransportPlane;
	public float SpawnDelay = 2.0f;
	public bool AutoSpawn = true;
	public int PlaneCount => planes.Count;

	private float delay;
	private Transform spawnLeft;
	private Transform spawnRight;
	private List<Transform> planes;

	void Start ()
	{
		delay = SpawnDelay;
		spawnLeft = transform.Find("SpawnLeft");
		spawnRight = transform.Find("SpawnRight");
		planes = new List<Transform>(2);
	}
	
	void Update ()
	{
		if (!AutoSpawn) return;
		
		delay -= Time.deltaTime;
		
		if (planes.Count == 0 && delay < 0)
		{
			if (Random.value < 0.5f)
			{
				SpawnLeft();
			}
			else
			{
				SpawnRight();
			}
		}
	}

	public void SpawnLeft()
	{
		var plane = Spawn(spawnLeft);
		plane.GetComponent<FlyHorizontal>().FlyRight();
	}

	public void SpawnRight()
	{
		var plane = Spawn(spawnRight);
		plane.GetComponent<FlyHorizontal>().FlyLeft();
	}

	private Transform Spawn(Transform spawn)
	{
		delay = SpawnDelay;
		
		return Instantiate(TransportPlane, spawn.position, Quaternion.identity);
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		var plane = other.GetComponent<FlyHorizontal>();
		if (plane != null)
		{
			planes.Add(other.transform);
		}
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		var plane = other.GetComponent<FlyHorizontal>();
		if (plane != null)
		{
			planes.Remove(other.transform);
		}
	}
}
