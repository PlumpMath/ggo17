using UnityEditor.Experimental.AssetImporters;
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

	public void ReadyUp()
	{
		cover = false;
		
		animator.SetBool("Cover", cover);
	}

	public void TakeCover()
	{
		cover = true;
		
		animator.SetBool("Cover", cover);
	}

	public void ToggleCover()
	{
		cover = !cover;
		
		animator.SetBool("Cover", cover);
	}

	public void ReadyArm()
	{
		armJoint.gameObject.SetActive(true);
		armJoint.transform.Rotate(0, 0, 270);
		aimed = true;
	}

	public void UnreadyArm()
	{
		armJoint.gameObject.SetActive(false);
		armJoint.transform.Rotate(0, 0, 0);
		aimed = false;
	}
}
