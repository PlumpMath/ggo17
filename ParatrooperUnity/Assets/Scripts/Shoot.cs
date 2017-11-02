using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{

	public Transform BulletPrefab;

	private float _coolDown = 0.0f;
	private Transform _turret;
	private Transform _bulletSpawn;
	
	void Start ()
	{
		_turret = transform.Find("Turret");
		_bulletSpawn = _turret.Find("Gun").Find("BulletSpawn");

	}
	
	void Update ()
	{
		_coolDown -= Time.deltaTime;

		if (_coolDown > 0) return;
		
		if (Input.GetKeyDown(KeyCode.Space))
		{
			Fire();
		}
	}

	private void Fire()
	{
		_coolDown = 1.0f;

		var direction = _bulletSpawn.position - _turret.position;
		var spawn = _bulletSpawn.position;
		var bullet = Instantiate(BulletPrefab, spawn, Quaternion.identity);
		bullet.GetComponent<Rigidbody2D>().AddForce((spawn - direction) * 1500);
	}
}
