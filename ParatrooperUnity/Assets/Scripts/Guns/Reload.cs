using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reload : MonoBehaviour {

	[SerializeField]
	private Gun gun;

	void Update()
	{
		if (Input.GetKey(KeyCode.R))
		{
			gun.Reload();
		}
	}
}
