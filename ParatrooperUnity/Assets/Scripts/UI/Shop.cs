using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{

	[SerializeField]
	private BunkerManager bunkerManager;

	[SerializeField]
	private Text PointsValue;
	
	void Update ()
	{
		if(this.PointsValue != null)
		{
			this.PointsValue.text = this.bunkerManager.Points + "";
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
		Debug.Log("WOHOO!");
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
