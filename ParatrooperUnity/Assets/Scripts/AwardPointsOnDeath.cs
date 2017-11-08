using UnityEngine;


public class AwardPointsOnDeath : MonoBehaviour
{

	public int Points = 1;
	
	private void OnDestroy()
	{
		PointsManager.AddPoints(Points);
	}
}
