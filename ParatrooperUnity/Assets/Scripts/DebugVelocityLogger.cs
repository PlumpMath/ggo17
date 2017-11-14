using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class DebugVelocityLogger : MonoBehaviour {
	
	private Rigidbody2D body;

	void Awake()
	{
		body = GetComponent<Rigidbody2D>();
	}
	
	void Update() {
		Debug.Log(gameObject.name + " V: " + Math.Round(body.velocity.magnitude, 2));
	}
}
