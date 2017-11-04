using UnityEngine;

public class Health : MonoBehaviour
{

	public float HP = 10.0f;

	void Update () {
		if (HP < 0)
		{
			Destroy(gameObject);
		}
	}

	public void Damage(float dmg)
	{
		HP -= dmg;
	}
}
