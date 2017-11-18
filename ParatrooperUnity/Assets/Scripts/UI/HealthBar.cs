using UnityEngine;

public class HealthBar : MonoBehaviour
{

	[SerializeField]
	private float health = 10.0f;
	[SerializeField]
	private float maxHealth = 10.0f;
	
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
		bar.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, maxWidth / maxHealth * health);
	}
}
