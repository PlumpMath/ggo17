using UnityEngine;

[RequireComponent(typeof(AudioSource), typeof(Rigidbody2D))]
public class Parachute : MonoBehaviour, IRecyclable
{

	public float Drag = 120.0f;
	public float DeployTime = 0.25f;

	[SerializeField]
	private AudioClip openSound;
	
	public bool IsOpen => open;

	private Rigidbody2D body;
	private AudioSource audioSource;
	private bool open = false;
	private float deploy = 0.0f;

	private void Awake()
	{	
		body = GetComponent<Rigidbody2D>();
		audioSource = GetComponent<AudioSource>();
	}

	void Start()
	{
		ScaleParameters(0.0f);
	}
	
	void Update() {
		if (open && deploy < DeployTime)
		{
			deploy += Time.deltaTime;
			
			ScaleParameters(deploy / DeployTime);
		}
	}

	public void Open()
	{
		open = true;
		audioSource.PlayOneShot(openSound, 0.8f);
		audioSource.Play();
	}

	public void Stash()
	{
		ScaleParameters(0);
	}

	private void ScaleParameters(float scale)
	{
		body.drag = Drag * scale;
		transform.localScale = new Vector3(scale, scale, scale);
	}

	public void Recycle()
	{
		open = false;
		deploy = 0.0f;
		ScaleParameters(0);
	}
}
