using UnityEngine;

[RequireComponent(typeof(Pooled))]
public class Damage : MonoBehaviour
{

	public float Amount => HP;
	public DamageSource Source => source;
	
	[SerializeField]
	private float HP = 50.0f;

	[SerializeField]
	[Tooltip("Source of the damage - typically used for scoring.")]
	private DamageSource source;

	private Pooled pooled;

	private void Awake()
	{
		pooled = GetComponent<Pooled>();
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		var health = other.GetComponent<Health>();
		if (health != null)
		{
			health.Damage(this);

			DestroySelf();
		}
	}

	private void DestroySelf()
	{
		pooled.DestroyPooled();
	}
}
