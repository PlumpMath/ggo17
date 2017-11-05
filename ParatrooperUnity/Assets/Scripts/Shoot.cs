using UnityEngine;

public class Shoot : MonoBehaviour
{

	public Transform BulletPrefab;
	public Transform GunJoint;
	public Transform BulletSpawn;
	public float ShotVelocity = 1000.0f;
	public float RechargeSeconds = 0.5f;

	[SerializeField]
	private AudioClip pew;

	private float _coolDown = 0.0f;

	private AudioSource bulletSpawnAudioSource;
	
	
	void Start ()
	{
		bulletSpawnAudioSource = BulletSpawn.GetComponent<AudioSource>();
	}
	
	void Update ()
	{
		_coolDown -= Time.deltaTime;

		if (_coolDown > 0) return;
		
		if (Input.GetKey(KeyCode.UpArrow))
		{
			Fire();
		}
	}

	private void Fire()
	{
		_coolDown = RechargeSeconds;

		var direction = (BulletSpawn.position - GunJoint.position).normalized;
		var spawnPos = BulletSpawn.position;
		var bullet = Instantiate(BulletPrefab, spawnPos, Quaternion.identity);
		bullet.GetComponent<Rigidbody2D>().AddForce(direction * ShotVelocity);

		bulletSpawnAudioSource.PlayOneShot(this.pew);
	}
}
