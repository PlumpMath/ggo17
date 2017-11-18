using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class GravityIsABitch : MonoBehaviour
{

	public float BaseDamageMultiplier = 1.5f;
	public float MaxDamageMultiplier = 2.0f;
	public bool DebuggingOn = false;
	
	private void OnCollisionEnter2D(Collision2D other)
	{
		var collidee = other.gameObject;
		var collisionV = other.relativeVelocity.magnitude;

		if (collisionV < 3.0)
		{
			if (DebuggingOn)
			{
				Debug.Log("SAFE LANDING " + collidee.name);
			}
			
			return;
		}
		
		var health = collidee.GetComponent<Health>();

		if (health != null)
		{
			var baseDamage = collisionV * BaseDamageMultiplier;
			var randomDamage = Random.value * (MaxDamageMultiplier - BaseDamageMultiplier) * collisionV;
			var totalDamage = (float) Math.Round(baseDamage + randomDamage, 2);
			health.Damage(totalDamage);
			
			if (DebuggingOn)
			{
				Debug.Log("SPLAT " + collidee.name + ": V = " + Math.Round(collisionV, 2) + "; DMG = " + totalDamage);
			}
		}
	}
}
