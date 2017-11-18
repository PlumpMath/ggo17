using UnityEngine;

public class AnimateMeleeCloud : MonoBehaviour
{

	[SerializeField]
	private float minRotation = -15.0f;
	[SerializeField]
	private float maxRotation = +15.0f;
	[SerializeField]
	private float rotationRate = 10.0f;
	
	private float rotation = 0.0f;
	[SerializeField]
	private float rotationDirection = 1.0f;
	
	[SerializeField]
	private float minScale = 0.8f;
	[SerializeField]
	private float maxScale = 1.2f;
	[SerializeField]
	private float scaleRate = 0.4f;
	
	private float scale = 1.0f;
	[SerializeField]
	private float scaleDirection = 1.0f;
	
	void Update()
	{
		UpdateRotation();

		UpdateScale();
	}

	private void UpdateRotation()
	{
		var rotationDelta = rotationRate * rotationDirection * Time.deltaTime;
		rotation += rotationDelta;
		transform.Rotate(0, 0, rotationDelta);

		if ((rotation < minRotation && rotationDirection < 0.0f) || (rotation > maxRotation && rotationDirection > 0.0f))
		{
			rotationDirection = -rotationDirection;
		}
	}
	
	private void UpdateScale()
	{
		scale += scaleRate * scaleDirection * Time.deltaTime;
		transform.localScale = new Vector3(scale, scale, 1.0f);

		if ((scale < minScale && scaleDirection < 0.0f) || (scale > maxScale && scaleDirection > 0.0f))
		{
			scaleDirection = -scaleDirection;
		}
	}
}
