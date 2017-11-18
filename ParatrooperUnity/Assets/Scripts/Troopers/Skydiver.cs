using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Skydiver : MonoBehaviour {

	public bool DebuggingOn = false;

	private Rigidbody2D body;
	private Parachute chute;
	
	void Awake()
	{
		body = GetComponent<Rigidbody2D>();
		chute = GetComponentInChildren<Parachute>();

		if (DebuggingOn && chute == null)
		{
			Debug.Log("OH SHIT - NO CHUTE: " + gameObject.name);
		}
	}
	
	void Update() {
		if (gameObject.transform.position.y < 0.0f)
		{
			if (!chute.IsOpen)
			{
				chute.Open();				
			}

			if (chute.IsOpen && Math.Abs(body.velocity.y) < 0.01f)
			{
				chute.Stash();
			}
		}	
	}
}
