using UnityEngine;

[RequireComponent(typeof(Animator), typeof(Health))]
public class BunkerDamage : MonoBehaviour
{

	private Health health;
	private Animator animator;
	
	void Start ()
	{
		this.health = this.GetComponent<Health>();
		this.animator = this.GetComponent<Animator>();
	}
	
	void Update () {
		this.animator.SetInteger("DamageLevel", Mathf.FloorToInt((1.0f - this.health.Percentage) * 5.9f));
	}
}
