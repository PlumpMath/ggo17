﻿using UnityEngine;

public class Shoot : MonoBehaviour
{
	[SerializeField]
	private Transform gunJoint;
	[SerializeField]
	private Gun gun;

	void Update()
	{
		if (Input.GetKey(KeyCode.UpArrow))
		{
			gun.Fire(gunJoint);
		}
	}
}