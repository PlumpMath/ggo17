using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{

	[SerializeField]
	private BunkerManager bunkerManager;

	[SerializeField]
	private Text PointsValue;
	
	[SerializeField]
	private Text GuardCostLeft;
	
	[SerializeField]
	private Text GuardCostRight;
	
	void Update ()
	{
		if(this.PointsValue != null)
		{
			this.PointsValue.text = this.bunkerManager.Points + "";
		}
		if(this.GuardCostLeft != null)
		{
			this.GuardCostLeft.text = this.bunkerManager.GuardPrice + "";
		}
		if(this.GuardCostRight != null)
		{
			this.GuardCostRight.text = this.bunkerManager.GuardPrice + "";
		}
		
	}

	public void QuitGame()
	{
		Application.Quit();
	}

	public void TitleMenu()
	{
		SceneManager.LoadScene("Title");
	}
	
	public void ResetGame()
	{
		this.bunkerManager.NewGame();
		SceneManager.LoadScene("Shop");
	}
	
	public void BuyLeftGuard()
	{
		this.bunkerManager.BuyLeftGuard();
	}

	public void BuyRightGuard()
	{
		this.bunkerManager.BuyRightGuard();
	}

	public void BeginGame()
	{
		SceneManager.LoadScene("Main");
	}
}
