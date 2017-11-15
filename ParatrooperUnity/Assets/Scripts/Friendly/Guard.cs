using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Guard : MonoBehaviour
{

	private Animator animator;
	private Transform armJoint;
	private Gun gun;
	
	private bool cover = true;
	private bool aimed = false;
	
	void Awake()
	{
		animator = GetComponent<Animator>();
		armJoint = transform.Find("ArmJoint");
		armJoint.gameObject.SetActive(false);
		gun = armJoint.GetComponentInChildren<Gun>();
	}
	
	void Update() {
		if (!cover && aimed)
		{
			gun.Fire(armJoint);
		}
	}

	public void ToggleCover()
	{
		if (cover)
		{
			ReadyUp();
		}
		else
		{
			TakeCover();
		}
	}
	
	public void ReadyUp()
	{
		cover = false;
		animator.SetBool("Cover", cover);
	}

	public void TakeCover()
	{
		StartCoroutine(RotateArmToUnready());
	}

	public void ReadyArm()
	{
		StartCoroutine(RotateArmToReady());
	}

	public void UnreadyArm()
	{
		armJoint.gameObject.SetActive(false);
	}

	IEnumerator RotateArmToReady()
	{
		armJoint.gameObject.SetActive(true);
		var rotation = 0.0f;
		var ratePerSecond = -90.0f;
		while (rotation > -90.0f)
		{
			var amount = ratePerSecond * Time.deltaTime;
			armJoint.transform.Rotate(0, 0, amount);
			rotation += amount;
			
			yield return null;
		}

		aimed = true;
	}
	
	IEnumerator RotateArmToUnready()
	{
		aimed = false;
		
		var rotation = -90.0f;
		var ratePerSecond = 90.0f;
		while (rotation < 0.0f)
		{
			var amount = ratePerSecond * Time.deltaTime;
			armJoint.transform.Rotate(0, 0, amount);
			rotation += amount;
			
			yield return null;
		}

		cover = true;
		animator.SetBool("Cover", cover);
	}
}
