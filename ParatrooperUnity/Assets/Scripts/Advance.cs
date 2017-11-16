using System;
using System.Collections;
using UnityEngine;

/*
 * This script is extremely crap... consider it a placeholder.
 *
 * Just needed something to test guards properly.
 */
[RequireComponent(typeof(Rigidbody2D))]
public class Advance : MonoBehaviour, IRecyclable
{

	private Rigidbody2D body;
	private int onGroundCount = 0;
	private bool OnGround = false;
	private bool Advancing = false;
	
	void Awake()
	{
		body = GetComponent<Rigidbody2D>();
	}

	void Update()
	{
		bool nowOnGround = Math.Abs(body.velocity.y) < 0.01f;
		if (nowOnGround)
		{
			onGroundCount++;
		}

		if (!OnGround && nowOnGround && onGroundCount > 10)
		{
			Debug.Log("Landed... advancing...");

			StartCoroutine(Posture());
		}

		if (Advancing)
		{
			body.velocity = new Vector3(0 - body.position.x, 0, 0).normalized * 0.3f; 
		}
	}

	IEnumerator Posture()
	{
		// TODO: Should be a smooth animation instead...
		
		yield return null;
		
		body.MoveRotation(0);
		body.freezeRotation = true;
		Advancing = true;
	}

	public void Recycle()
	{
		body.freezeRotation = false;
		Advancing = false;
		OnGround = false;
		onGroundCount = 0;
	}
}
