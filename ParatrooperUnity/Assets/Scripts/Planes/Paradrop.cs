using UnityEngine;

public class Paradrop : MonoBehaviour
{

	[SerializeField]
	private Transform paratrooperPrefab;
	
	[SerializeField]
	private float minDropDelay = 1.0f;
	
	[SerializeField]
	private float maxDropDelay = 2.0f;
	
	[SerializeField]
	private int maxDrops = 2;
	
	private Transform spawn;
	private int dropped = 0;
	private float timer;
	
	void Start ()
	{
		spawn = transform.Find("Spawn");

		SetJumpTimer();
	}

	void Update ()
	{
		if (dropped == maxDrops) return;
		
		timer -= Time.deltaTime;

		if (timer <= 0)
		{
			Drop();
		}
	}
	
	private void Drop()
	{
		PoolingFactory.SpawnOrRecycle(paratrooperPrefab, spawn.position);
		
		dropped++;
		
		SetJumpTimer();
	}
	
	private void SetJumpTimer()
	{
		timer = minDropDelay + (maxDropDelay - minDropDelay) * Random.value;
	}
}
