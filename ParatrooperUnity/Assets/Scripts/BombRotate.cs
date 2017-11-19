using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BombRotate : MonoBehaviour
{
	private Rigidbody2D body;

	private void Awake()
	{
		this.body = this.GetComponent<Rigidbody2D>();
		this.RotateToVelocity();
	}
	
	void Update ()
	{
		this.RotateToVelocity();
	}

	private void RotateToVelocity()
	{
		var angle = Vector2.Angle(Vector2.right, this.body.velocity);

		this.body.rotation = -angle + 180.0f;
	}
}
