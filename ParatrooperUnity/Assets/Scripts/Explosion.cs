using UnityEngine;

[RequireComponent(typeof(ShrapnelBlast))]
[RequireComponent(typeof(Shockwave))]
public class Explosion : MonoBehaviour, IRecyclable
{
    private ShrapnelBlast shrapnelBlast;
    
    private void Awake()
    {
        this.shrapnelBlast = this.GetComponent<ShrapnelBlast>();
        this.Recycle();
    }

    public void Recycle()
    {
        this.shrapnelBlast.Trigger();
    }
}    