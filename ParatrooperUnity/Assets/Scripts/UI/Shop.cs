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
    private Text DayValue;

    [SerializeField]
    private Text GuardCostLeft;

    [SerializeField]
    private Text GuardCostRight;

    [SerializeField]
    private Text FlakCost;

    [SerializeField]
    private Text BunkerHP;

    [SerializeField]
    private Text RepairCost;

    void Update()
    {
        if(this.PointsValue != null)
        {
            this.PointsValue.text = this.bunkerManager.Points + "";
        }
        if(this.DayValue != null)
        {
            this.DayValue.text = this.bunkerManager.Day + "";
        }
        if(this.GuardCostLeft != null)
        {
            this.GuardCostLeft.text = this.bunkerManager.LeftGuard ? "-" : this.bunkerManager.GuardPrice + "";
        }
        if(this.GuardCostRight != null)
        {
            this.GuardCostRight.text = this.bunkerManager.RightGuard ? "-" : this.bunkerManager.GuardPrice + "";
        }
        if(this.FlakCost != null)
        {
            this.FlakCost.text = this.bunkerManager.Flak ? "-" : this.bunkerManager.FlakPrice + "";
        }
        if(this.BunkerHP != null)
        {
            this.BunkerHP.text = "(" + this.bunkerManager.HitPoints + "/" + this.bunkerManager.InitialHitPoints + ")";
        }
        if(this.RepairCost != null)
        {
            this.RepairCost.text = this.bunkerManager.RepairPrice + "";
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

    public void BuyFlak()
    {
        this.bunkerManager.BuyFlak();
    }

    public void BuyRepair()
    {
        this.bunkerManager.BuyRepair();
    }
    
    public void BeginGame()
    {
        PointsManager.Instance.Points = this.bunkerManager.Points;
        SceneManager.LoadScene("Main");
    }
}
