using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ShrapnelAnimation : MonoBehaviour
{

	[SerializeField]
	[Tooltip("Minimum rotation per second in degrees.")]
	private float minRotationPerSecond;

	[SerializeField]
	[Tooltip("Maximum rotation per second in degrees.")]
	private float maxRotationPerSecond;
	
	[SerializeField]
	[Tooltip("Minimum scale.")]
	private float minScale;
	
	[SerializeField]
	[Tooltip("Maximum scale.")]
	private float maxScale;

	private float rotation = 0.0f;
	
	void Awake()
	{
		this.rotation = Random.Range(this.minRotationPerSecond, this.maxRotationPerSecond) * Random.Range(-1, 2);

		var scale = Random.Range(this.minScale, this.maxScale);
		this.transform.localScale = new Vector3(scale, scale, 1);
	}
	
	void Update () {
		this.transform.Rotate(0, 0, this.rotation);
	}
}
