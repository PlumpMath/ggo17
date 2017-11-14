using UnityEngine;

[RequireComponent(typeof(Pooled))]
public class Damage : MonoBehaviour
{

	public float HP = 50.0f;

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
			health.Damage(HP);

			pooled.DestroyPooled();
		}
	}
}
