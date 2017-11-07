using UnityEngine;

public class Parachute : MonoBehaviour
{

	public float Drag = 120.0f;
	public float DeployTime = 0.25f;
	public bool IsOpen => _open;

	private Rigidbody2D _body;
	private bool _open = false;
	private float _deploy = 0.0f;

	void Start ()
	{
		_body = GetComponent<Rigidbody2D>();
		ScaleParameters(0.0f);
	}
	
	void Update () {
		if (_open && _deploy < DeployTime)
		{
			_deploy += Time.deltaTime;
			
			ScaleParameters(_deploy / DeployTime);
		}
	}

	public void Open()
	{
		_open = true;
	}

	public void Stash()
	{
		ScaleParameters(0);
	}

	private void ScaleParameters(float scale)
	{
		if (_body != null)
		{
			_body.drag = Drag * scale;
			transform.localScale = new Vector3(scale, scale, scale);
		}
	}
}
