using System;
using UnityEngine;

public class Rotate : MonoBehaviour
{

	public Transform GunJoint;
	public float DegreesPerSecond = 60.0f;

	private float _rotation = 0.0f;
	private float _change = 0.0f;
	
	void Update ()
	{
		if (Input.GetKey(KeyCode.LeftArrow))
		{
			_change = DegreesPerSecond * Time.deltaTime;
		}
		else if (Input.GetKey(KeyCode.RightArrow))
		{
			_change = -DegreesPerSecond * Time.deltaTime;
		}
		else
		{
			_change = 0.0f;
		}
	}

	void FixedUpdate()
	{
		if (Math.Abs(_change) < 0.01f) return;

		var endZ = _rotation + _change;
		if (endZ >= 90.0f)
		{
			_change -= endZ - 90.0f;
		}
		else if (endZ <= -90.0f)
		{
			_change -= endZ + 90.0f;
		}

		_rotation += _change;
		GunJoint.transform.rotation = Quaternion.Euler(0, 0, _rotation);
	}
}
