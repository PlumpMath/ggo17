using System;
using UnityEngine;

/* Global game state manager */
[CreateAssetMenu(menuName = "State/BunkerManager")]
public class BunkerManager : ScriptableObject //SingletonSO<BunkerManager>
{
    public const int StartPoints = 1000;
    
    public int Points { get; private set; } = StartPoints;
    public int Day { get; private set; } = 1;

    public int GuardBasePrice { get; set; } = 600;
    public float GuardPriceIncrease { get; set; } = 2.0f;
    public int GuardsBought { get; private set; } = 0;

    public bool LeftGuard { get; private set; } = false;
    public bool RightGuard { get; private set; } = false;

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
}
