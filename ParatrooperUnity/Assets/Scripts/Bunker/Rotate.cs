using System;
using UnityEngine;

public class Rotate : MonoBehaviour
{

	public Transform GunJoint;
	public float DegreesPerSecond = 60.0f;

	private float rotation = 0.0f;
	private float change = 0.0f;
	
	void Update()
	{
		if (Input.GetKey(KeyCode.LeftArrow))
		{
			change = DegreesPerSecond * Time.deltaTime;
		}
		else if (Input.GetKey(KeyCode.RightArrow))
		{
			change = -DegreesPerSecond * Time.deltaTime;
		}
		else
		{
			change = 0.0f;
		}
	}

	void FixedUpdate()
	{
		if (Math.Abs(change) < 0.01f) return;

		var endZ = rotation + change;
		if (endZ >= 90.0f)
		{
			change -= endZ - 90.0f;
		}
		else if (endZ <= -90.0f)
		{
			change -= endZ + 90.0f;
		}

		rotation += change;
		GunJoint.transform.rotation = Quaternion.Euler(0, 0, rotation);
	}
}
