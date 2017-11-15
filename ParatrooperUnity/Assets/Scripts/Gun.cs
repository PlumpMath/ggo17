using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Gun : MonoBehaviour
{
	[SerializeField]
	private Bullet bulletPrefab;
	[SerializeField]
	private Transform bulletSpawn;
	[SerializeField]
	private float shotVelocity = 1000.0f;
	[SerializeField]
	private float rechargeSeconds = 0.5f;
	[SerializeField]
	private int maxAmmo = 10;
	[SerializeField]
	private float reloadDuration = 2.0f;
	[SerializeField]
	private AudioClip pew;
	[SerializeField]
	private AudioClip click;

	private int ammo = 0;
	private bool reloading = false;
	private float coolDown = 0.0f;
	private AudioSource gunAudioSource;

	private void Awake()
	{
		ammo = maxAmmo;
		gunAudioSource = GetComponent<AudioSource>();
	}

	private void Update()
	{
		coolDown -= Time.deltaTime;
	}

	public void Fire(Transform gunJoint)
	{
		if (coolDown > 0.0f)
		{
			return;
		}

		if (reloading || maxAmmo > 0 && ammo <= 0)
		{
			gunAudioSource.PlayOneShot(click);
			coolDown = rechargeSeconds;
			return;
		}
		
		coolDown = rechargeSeconds;

		var direction = (Vector2)(bulletSpawn.position - gunJoint.position).normalized;
		var spawnPos = bulletSpawn.position;
		var bullet = PoolingFactory.SpawnOrRecycle<Bullet>(bulletPrefab.transform, spawnPos);
		bullet.AddForce(direction * shotVelocity);

		if (maxAmmo > 0)
		{
			ammo--;
		}

		gunAudioSource.PlayOneShot(pew);
	}

	public void Reload()
	{
		if (reloading)
		{
			return;
		}
		
		reloading = true;

		StartCoroutine(WaitForReload());
	}

	IEnumerator WaitForReload()
	{
		float progress = 0.0f;
		while (progress < reloadDuration)
		{
			progress += Time.deltaTime;

			yield return null;
		}

		reloading = false;
		ammo = maxAmmo;
		coolDown = 0.0f;
	}
}