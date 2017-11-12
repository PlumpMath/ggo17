using UnityEngine;

public class Damage : MonoBehaviour
{

	public float HP = 50.0f;

	private void OnTriggerEnter2D(Collider2D other)
	{
		var health = other.GetComponent<Health>();
		if (health != null)
		{
			health.Damage(HP);

			DestroySelf();
		}
	}

	private void DestroySelf()
	{
		var pooled = gameObject.GetComponent<Pooled>();
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
