using UnityEngine;

public class HealthBar : MonoBehaviour
{

	public float Health { get; set; }= 10.0f;
	public float MaxHealth { get; set; }= 10.0f;
	
	private RectTransform bar;
	private float maxWidth = 0.0f;
	
	void Awake()
	{
		var barTransform = transform.Find("Bar");
		if (barTransform != null)
		{
			bar = barTransform.GetComponent<RectTransform>();
		}

		if (bar != null)
		{
			maxWidth = bar.rect.width;
		}
	}
	
	void Update ()
	{
		bar.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, maxWidth / MaxHealth * Health);
	}
}
