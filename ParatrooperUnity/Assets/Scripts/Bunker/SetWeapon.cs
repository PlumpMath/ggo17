using UnityEngine;

[RequireComponent(typeof(Gun))]
public class SetWeapon : MonoBehaviour
{

	[SerializeField]
	private Bullet defaultShot;

	[SerializeField]
	private Bullet flakShot;
	
	[SerializeField]
	private BunkerManager bunkerManager;
	
	void Awake()
	{
		var gun = GetComponent<Gun>();
		if(this.bunkerManager.Flak)
		{
			gun.SetAmmo(this.flakShot);
		}
		else
		{
			gun.SetAmmo(this.defaultShot);
		}
	}
}
