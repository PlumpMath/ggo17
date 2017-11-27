using System;
using UnityEngine;
using UnityEngine.SceneManagement;

/* Global game state manager */
[CreateAssetMenu(menuName = "State/BunkerManager")]
public class BunkerManager : ScriptableObject //SingletonSO<BunkerManager>
{
    public const int StartPoints = 0;
    public const int InitialHitPoints = 500;
    
    public int Points { get; private set; } = StartPoints;
    public int Day { get; private set; } = 1;
    public float DayDurationSeconds { get; private set; } = 60.0f;

    public int HitPoints { get; private set; } = InitialHitPoints; 

    public int GuardBasePrice { get; set; } = 600;
    public float GuardPriceIncrease { get; set; } = 2.0f;
    public int GuardsBought { get; private set; } = 0;

    public bool LeftGuard { get; private set; } = false;
    public bool RightGuard { get; private set; } = false;

    public void SurvivedDay(int bunkerHP)
    {
        Day++;
        Points = Mathf.FloorToInt(PointsManager.Instance.Points);
        HitPoints = bunkerHP;
        SceneManager.LoadScene("Shop");
    }
    
    public int GuardPrice
    {
        get
        {
            var result = GuardBasePrice;
            for(int i = 0; i < GuardsBought; i++)
            {
                result = (int)Math.Round(result * GuardPriceIncrease);
            }
            return result;
        }
    }

    public void NewGame()
    {
        Points = StartPoints;
        Day = 1;
        DayDurationSeconds = 60.0f;
        HitPoints = InitialHitPoints;

        GuardsBought = 0;
        LeftGuard = false;
        RightGuard = false;
    }

    public void BuyLeftGuard()
    {
        if(!LeftGuard && Points > GuardPrice)
        {
            Points -= GuardPrice;
            LeftGuard = true;
            GuardsBought++;
        }
    }

    public void BuyRightGuard()
    {
        if(!RightGuard && Points > GuardPrice)
        {
            Points -= GuardPrice;
            RightGuard = true;
            GuardsBought++;
        }
    }

    public void LeftGuardDied()
    {
        this.LeftGuard = false;
    }

    public void RightGuardDied()
    {
        this.RightGuard = false;
    }
}
