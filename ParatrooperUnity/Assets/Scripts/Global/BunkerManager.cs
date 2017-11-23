using UnityEngine;

[CreateAssetMenu(menuName = "Singletons/BunkerManager")]
public class BunkerManager : SingletonSO<BunkerManager>
{
    public int Points { get; private set; } = 1000;
    public bool LeftGuard { get; private set; } = false;
    public bool RightGuard { get; private set; } = false;

    public void NewGame()
    {
        Points = 1000;
        LeftGuard = false;
        RightGuard = false;
    }
    
    public void BuyLeftGuard()
    {
        if(Points > 600)
        {
            Points -= 600;
            LeftGuard = true;
        }
    }
    
    public void BuyRightGuard()
    {
        if(Points > 600)
        {
            Points -= 600;
            RightGuard = true;
        }
    }
}
