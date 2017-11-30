using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Guard : MonoBehaviour
{

	private static Color TransparentBlack = new Color(0, 0, 0, 0);
	
	private Animator animator;
	private Transform armJoint;
	private SpriteRenderer armRenderer;
	private Gun gun;

	private bool enabled = false;
	private bool cover = true;
	private bool aimed = false;
	private bool targets = false;
	
	void Awake()
	{
		this.InitComponents();
	}

	private void InitComponents()
	{
		this.animator = this.GetComponent<Animator>();
		this.armJoint = this.transform.Find("ArmJoint");
		this.armRenderer = this.armJoint.GetComponentInChildren<SpriteRenderer>();
		this.armRenderer.color = TransparentBlack;
		this.gun = this.armJoint.GetComponentInChildren<Gun>();

		this.gun.OnEmpty += this.TakeCover;
		this.gun.OnReloaded += this.ReadyUp;
	}

	void Update() {
		if (!cover && aimed && targets)
		{
			gun.Fire(armJoint);
			targets = false;
		}
	}
	
	private void OnTriggerStay2D(Collider2D other)
	{
		if (other.GetComponent<Health>()) // Assume soldier
		{
			targets = true;
		}
	}
	
	public void Enable()
	{
		enabled = true;
		ReadyUp();
	}
	
	public void Disable()
	{
		enabled = false;
		armRenderer.color = TransparentBlack;
		gun.InstantReload();
		cover = true;
		animator.SetBool("Cover", cover);
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

		if(this.animator == null)
		{
			this.InitComponents();
		}
		
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
		armRenderer.color = TransparentBlack;
	}

	IEnumerator RotateArmToReady()
	{
		armRenderer.color = Color.white;
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
		
		if (!enabled) yield break; // Assume the guard has died, so skip the reload that restarts the cycle
		gun.Reload();
	}
}
