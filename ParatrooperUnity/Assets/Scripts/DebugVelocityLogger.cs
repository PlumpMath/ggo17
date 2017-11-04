using System;
using UnityEngine;

public class DebugVelocityLogger : MonoBehaviour {
	
	private Rigidbody2D _body;

	void Start ()
	{
		_body = GetComponent<Rigidbody2D>();
	}
	
	void Update () {
		if (_body == null)
		{
			enabled = false;
		}
		
		Debug.Log(gameObject.name + " V: " + Math.Round(_body.velocity.magnitude, 2));
	}
}
