using UnityEngine;


public class Points : MonoBehaviour
{

	public int Value = 1;
	
	private void OnDestroy()
	{
		this.AwardPoints();
	}

	public void OnDestroyPooled()
	{
		this.AwardPoints();
	}

	private bool MustAwardPoints()
	{
		if(!gameObject.activeSelf) return false; // Prevents double allocation if 2 things destroy the object at once
		
		var health = GetComponent<Health>();
		
		if(health == null) return false;

		if(health.LastDamageFrom == DamageSource.Unknown) return false;

		return true;
	}

	private void AwardPoints()
	{
		if(this.MustAwardPoints())
		{
			PointsManager.Instance.AddPoints(this.Value);

			var health = GetComponent<Health>();
			if(name.StartsWith("Trooper"))
			{
				if(health.LastDamageFrom == DamageSource.Environment)
				{
					PointsManager.Instance.AddTrooperFell();
				}
				else if(health.LastDamageFrom == DamageSource.Player)
				{
					PointsManager.Instance.AddTrooperKill();
				}
			}
			else if(name.StartsWith("Transport"))
			{
				PointsManager.Instance.AddTransportKill();
			}
		}
	}
}
