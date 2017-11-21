using UnityEngine;

public class ShrapnelBlast : MonoBehaviour, IRecyclable
{
    [SerializeField]
    private Rigidbody2D shrapnelPrefab;

    [SerializeField]
    private int numberOfProjectiles;

    [SerializeField]
    private float forceOnShrapnel;

    private bool blasted = false;

    void Update()
    {
        if(!this.blasted)
        {
            this.blasted = true;
            this.Trigger();
        }
    }
    
    private void Trigger()
    {
        for(var i = 0; i < this.numberOfProjectiles; i++)
        {
            var shrapnel = PoolingFactory.SpawnOrRecycle<Rigidbody2D>(this.shrapnelPrefab.transform, this.transform.position);

            ApplyForceFromblast(shrapnel);
        }
    }

    private void ApplyForceFromblast(Rigidbody2D shrapnel)
    {
        var forceDirection = Random.insideUnitCircle;
        var forceDampener = Random.Range(0.7f, 1.0f);
        shrapnel.AddForce(forceDirection * forceDampener * this.forceOnShrapnel);
        
        // TODO: Adding a random rotation might look nice...
    }

    public void Recycle()
    {
        this.blasted = false;
    }
}