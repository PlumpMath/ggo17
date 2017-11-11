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
	private AudioClip pew;
	
	private float coolDown = 0.0f;
	private AudioSource gunAudioSource;

	private void Start()
	{
		this.gunAudioSource = this.GetComponent<AudioSource>();
	}

	private void Update()
	{
		this.coolDown -= Time.deltaTime;
	}

	public void Fire(Transform gunJoint)
	{
		if(this.coolDown > 0.0f)
		{
			return;
		}
		
		coolDown = rechargeSeconds;

		var direction = (Vector2)(bulletSpawn.position - gunJoint.position).normalized;
		var spawnPos = bulletSpawn.position;
		//var bullet = Instantiate(bulletPrefab, spawnPos, Quaternion.identity);
		var bullet = PoolingFactory.SpawnOrRecycle(bulletPrefab.transform, spawnPos);
		bullet.GetComponent<Bullet>().AddForce(direction * shotVelocity);

		gunAudioSource.PlayOneShot(this.pew);
	}
}