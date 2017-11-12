using UnityEngine;

public class Health : MonoBehaviour, IRecyclable
{

	[SerializeField]
	[Tooltip("Initial health.")]
	private float initialHitPoints = 10.0f;

	[SerializeField]
	[Tooltip("Current health. If not set will default to initial hit points. Death occurs at 0 HP.")]
	private float hitPoints;
	private Pooled pooled;

	void Start()
	{
		Recycle();

		pooled = GetComponent<Pooled>();
	}
	
	void Update () {
		if (hitPoints < 0)
		{
			DestroySelf();
		}
	}

	public void Damage(float dmg)
	{
		hitPoints -= dmg;
	}

	public void Recycle()
	{
		hitPoints = initialHitPoints;
	}

	private void DestroySelf()
	{
		if (pooled != null)
		{
			pooled.DestroyPooled();
		}
		else
		{
			Destroy(transform.gameObject);
		}
	}
}
