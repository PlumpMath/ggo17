using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Guard : MonoBehaviour
{

	private Animator animator;
	private Transform arm;
	
	private bool cover = true;
	
	
	void Awake()
	{
		animator = GetComponent<Animator>();
		arm = transform.Find("Arm");
		arm.gameObject.SetActive(false);
	}
	
	void Update() {
		
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
		arm.gameObject.SetActive(true);
	}

	public void UnreadyArm()
	{
		arm.gameObject.SetActive(false);
	}
}
