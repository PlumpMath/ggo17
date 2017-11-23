using System.Runtime.CompilerServices;
using UnityEngine;

public class Paradrop : MonoBehaviour, IRecyclable
{

	[SerializeField]
	private Transform paratrooperPrefab;
	
	[SerializeField]
	private float minDropDelay = 1.0f;
	
	[SerializeField]
	private float maxDropDelay = 2.0f;
	
	[SerializeField]
	private int maxDrops = 2;

	private Transform Spawn => this.transform;
	private int dropped;
	private float timer;
	
	void Awake()
	{
		Recycle();
	}
	
	public void Recycle()
	{
		SetJumpTimer();
		dropped = 0;
	}
	
	private void SetJumpTimer()
	{
		timer = minDropDelay + (maxDropDelay - minDropDelay) * Random.value;
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
		PoolingFactory.SpawnOrRecycle(paratrooperPrefab, this.Spawn.position);
		
		dropped++;
		
		SetJumpTimer();
	}
}
