using UnityEngine;

public class Pooled : MonoBehaviour, IRecyclable
{

	[SerializeField]
	[Tooltip("Value of 0.0f means live forever (or until something calls DestroyPooled).")]
	private float AfterTime;

	private bool timeBased;
	private float timeLeft;
	
	void Start () {
		Recycle();
	}
	
	void Update () {
		if (!timeBased) return;

		timeLeft -= Time.deltaTime;

		if (timeLeft <= 0.0f)
		{
			DestroyPooled();
		}
	}

	public void DestroyPooled()
	{
		var points = GetComponent<AwardPointsOnDeath>();
		if (points != null)
		{
			points.OnDestroyPooled();
		}

		gameObject.SetActive(false);
	}
	
	public void Recycle()
	{
		if (AfterTime > 0.01f)
		{
			timeBased = true;
			timeLeft = AfterTime;
		}
	}
}
