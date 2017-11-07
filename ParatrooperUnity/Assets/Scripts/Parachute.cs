using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Parachute : MonoBehaviour
{

	public float Drag = 120.0f;
	public float DeployTime = 0.25f;

	[SerializeField]
	private AudioClip openSound;
	
	public bool IsOpen => _open;

	private Rigidbody2D _body;
	private AudioSource audioSource;
	private bool _open = false;
	private float _deploy = 0.0f;

	private void Awake()
	{	
		_body = GetComponent<Rigidbody2D>();
		this.audioSource = this.GetComponent<AudioSource>();
	}

	void Start ()
	{
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
		this.audioSource.PlayOneShot(this.openSound, 0.8f);
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
