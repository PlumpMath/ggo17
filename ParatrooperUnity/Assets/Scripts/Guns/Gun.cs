using System.Collections;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

[RequireComponent(typeof(AudioSource))]
public class Gun : MonoBehaviour
{
	public delegate void OnEmptyAction();
	public delegate void OnReloadedAction();

	public event OnEmptyAction OnEmpty;
	public event OnReloadedAction OnReloaded;
	
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
	[SerializeField]
	private bool DebugOn = false;

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

	public void SetAmmo(Bullet bullet)
	{
		this.bulletPrefab = bullet;
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

			if (ammo == 0)
			{
				OnEmpty?.Invoke();
			}
		}
		
		gunAudioSource.PlayOneShot(pew);
	}

	public void Reload()
	{
		if (reloading)
		{
			return;
		}
		
		if (DebugOn)
		{
			Debug.Log("Reloading...");
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

		if (DebugOn)
		{
			Debug.Log("Reloaded!");
		}
		
		reloading = false;
		ammo = maxAmmo;
		coolDown = 0.0f;
		
		OnReloaded?.Invoke();
	}

	public void InstantReload()
	{
		// Instant reload that doesn't trigger an event.
		reloading = false;
		ammo = maxAmmo;
	}
}