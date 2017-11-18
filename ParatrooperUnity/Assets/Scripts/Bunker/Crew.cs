using System;
using UnityEngine;

public class Crew : MonoBehaviour {
	
	public delegate void OnDeathAction(Crew crew);
	public delegate void OnDamageAction(Crew crew, float damage);

	public event OnDeathAction OnDeath;
	public event OnDamageAction OnDamage;
	
	public Rank Rank => rank;
	public Role Role => role;
	public String Name => fullName;
	public float HitPoints => hitPoints;
	public float MaxHitPoints => maxHitPoints;
	public float HealthPercentage => hitPoints / maxHitPoints;
	
	[SerializeField]
	private Rank rank;
	[SerializeField]
	private Role role;
	[SerializeField]
	private String fullName;
	[SerializeField]	
	private float hitPoints;
	[SerializeField]
	private float maxHitPoints;
	

	public void Damage(float damage)
	{
		hitPoints -= damage;
		
		OnDamage?.Invoke(this, damage);

		if (hitPoints < 0)
		{
			OnDeath?.Invoke(this);
			
			Destroy(gameObject);
		}
	}
}
