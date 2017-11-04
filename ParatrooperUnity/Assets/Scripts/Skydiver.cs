using System;
using UnityEngine;

public class Skydiver : MonoBehaviour {

	public bool DebuggingOn = false;

	private Rigidbody2D _body;
	private Parachute _chute;
	
	void Start ()
	{
		_body = GetComponent<Rigidbody2D>();
		_chute = GetComponentInChildren<Parachute>();

		if (DebuggingOn && _chute == null)
		{
			Debug.Log("OH SHIT - NO CHUTE: " + gameObject.name);
		}
	}
	
	void Update () {
		if (gameObject.transform.position.y < 0.0f)
		{
			if (!_chute.IsOpen)
			{
				_chute.Open();				
			}

			if (_chute.IsOpen && Math.Abs(_body.velocity.y) < 0.01f)
			{
				_chute.Stash();
			}
		}	
	}
}
