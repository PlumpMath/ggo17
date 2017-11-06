using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FlyHorizontal : MonoBehaviour
{

	[SerializeField]
	private float Speed;
	
	void Start () {
		var body =  GetComponent<Rigidbody2D>();
		body.velocity = new Vector3(-Speed, 0, 0);
	}
	
	void Update () {
		
	}
}
