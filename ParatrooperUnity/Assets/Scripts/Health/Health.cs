using UnityEngine;

[RequireComponent(typeof(Pooled))]
public class Health : MonoBehaviour, IRecyclable
{

	public float Percentage => hitPoints / initialHitPoints;
	public float HitPoints => hitPoints;
	public DamageSource LastDamageFrom => lastDamage;
	
	[SerializeField]
	[Tooltip("Initial health.")]
	private float initialHitPoints = 10.0f;

	[SerializeField]
	[Tooltip("Current health. If not set will default to initial hit points. Death occurs at 0 HP.")]
	private float hitPoints;

	[SerializeField]
	[Tooltip("Optional destruction FX prefab.")]
	private Transform destructionPrefab;

	private DamageSource lastDamage;
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

	public void Damage(Damage dmg)
	{
		Damage(dmg.Amount, dmg.Source);
	}

	public void Damage(float dmg, DamageSource source = DamageSource.Unknown)
	{
		lastDamage = source;
		hitPoints -= dmg;
	}
	
	public void Recycle()
	{
		lastDamage = DamageSource.Unknown;
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
