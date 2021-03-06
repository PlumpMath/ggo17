﻿using UnityEngine;

[RequireComponent(typeof(Pooled))]
public class Damage : MonoBehaviour
{

	public float Amount => HP;
	public DamageSource Source => source;
	
	[SerializeField]
	protected float HP = 50.0f;

	[SerializeField]
	[Tooltip("Source of the damage - typically used for scoring.")]
	protected DamageSource source;

	[SerializeField]
	protected bool DebugOn = false;

	protected Pooled pooled;

	protected virtual void Awake()
	{
		pooled = GetComponent<Pooled>();
	}

	protected virtual void OnTriggerEnter2D(Collider2D other)
	{
		var health = other.GetComponent<Health>();
		if (health != null)
		{
			health.Damage(this);
			if (DebugOn) Debug.Log("Damaged " + other.name);

			DestroySelf();
		}
	}

	protected void DestroySelf()
	{
		pooled.DestroyPooled();
	}
}
