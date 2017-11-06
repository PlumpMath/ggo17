using UnityEngine;

public class Paradrop : MonoBehaviour
{

	public Transform Paratrooper;
	public float MinDropDelay = 1.0f;
	public float MaxDropDelay = 2.0f;
	public int MaxDrops = 2;
	
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
		if (dropped == MaxDrops) return;
		
		timer -= Time.deltaTime;

		if (timer <= 0)
		{
			Drop();
		}
	}
	
	private void Drop()
	{
		Instantiate(Paratrooper, spawn.position, Quaternion.identity);
		
		dropped++;
		
		SetJumpTimer();
	}
	
	private void SetJumpTimer()
	{
		timer = MinDropDelay + (MaxDropDelay - MinDropDelay) * Random.value;
	}
}
