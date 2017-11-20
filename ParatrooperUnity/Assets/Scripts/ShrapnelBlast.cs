using UnityEngine;

public class ShrapnelBlast : MonoBehaviour
{
    [SerializeField]
    private Shrapnel shrapnelPrefab;

    [SerializeField]
    private int numberOfProjectiles;

    [SerializeField]
    private float forceOnShrapnel;

    public void Trigger()
    {
        for(var i = 0; i < this.numberOfProjectiles; i++)
        {
            var shrapnel = PoolingFactory.SpawnOrRecycle<Shrapnel>(this.shrapnelPrefab.transform, this.transform.position);
            shrapnel.ApplyForceFromBlast(this.forceOnShrapnel);
        }
    }
}