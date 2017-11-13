using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class FlyHorizontal : MonoBehaviour
{

	[SerializeField]
	private float Speed;

	private Rigidbody2D body;
	private Vector2 direction = Vector2.left;
	
	void Awake() {
		body =  GetComponent<Rigidbody2D>();
	}

	void Update()
	{
		if (!body.velocity.Equals(direction))
		{
			body.velocity = direction;
		}
	}

	public void FlyLeft()
	{
		direction = Vector2.left * Speed;
		SetFacing(1);
	}

	public void FlyRight()
	{
		direction = Vector2.right * Speed;
		SetFacing(-1);
	}
	
	private void SetFacing(int facing)
	{
		var scale = transform.localScale;
		scale.x = facing;
		transform.localScale = scale;
	}
}
