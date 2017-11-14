using System.Collections.Generic;
using UnityEngine;


public class LaneManager : MonoBehaviour
{

	[SerializeField]
	private FlyHorizontal planePrefab;
	[SerializeField]
	private float spawnDelay = 2.0f;
	[SerializeField]
	private bool autoSpawn = true;
	
	public int PlaneCount => planes.Count;

	private float delay;
	private Transform spawnLeft;
	private Transform spawnRight;
	private List<Transform> planes;

	void Awake()
	{
		delay = spawnDelay;
		spawnLeft = transform.Find("SpawnLeft");
		spawnRight = transform.Find("SpawnRight");
		planes = new List<Transform>(2);
	}
	
	void Update()
	{
		if (!autoSpawn) return;
		
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
		plane.FlyRight();
	}

	public void SpawnRight()
	{
		var plane = Spawn(spawnRight);
		plane.FlyLeft();
	}

	private FlyHorizontal Spawn(Transform spawn)
	{
		delay = spawnDelay;

		return PoolingFactory.SpawnOrRecycle<FlyHorizontal>(planePrefab.transform, spawn.position);
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
			var pooled = other.transform.GetComponent<Pooled>();
			if (pooled != null)
			{
				pooled.DestroyPooled();
			}
		}
	}
}
