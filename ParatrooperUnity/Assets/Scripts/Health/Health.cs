using UnityEngine;

[RequireComponent(typeof(Pooled))]
public class Health : MonoBehaviour, IRecyclable
{

	public float Percentage => hitPoints / initialHitPoints;
	public float HitPoints => hitPoints;
	
	[SerializeField]
	[Tooltip("Initial health.")]
	private float initialHitPoints = 10.0f;

	[SerializeField]
	[Tooltip("Current health. If not set will default to initial hit points. Death occurs at 0 HP.")]
	private float hitPoints;

	[SerializeField]
	[Tooltip("Optional destruction FX prefab.")]
	private Transform destructionPrefab;
	
	private Pooled pooled;

	private void Awake()
	{
		pooled = GetComponent<Pooled>();
		
		Recycle();
	}

	void Update() {
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

		if (destructionPrefab != null)
		{
			var prefab = PoolingFactory.SpawnOrRecycle(destructionPrefab, transform.position);
			prefab.localScale = transform.localScale;
		}
	}
}
