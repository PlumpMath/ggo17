using UnityEngine;

public class ShrapnelBlast : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D shrapnelPrefab;

    [SerializeField]
    [Tooltip("Emit this number of projectiles in a circular pattern (e.g. for 12 at degrees: 0, 30, 60, etc.).")]
    private int numberOfProjectiles;

    [SerializeField]
    [Tooltip("Vary the exact angle by a random percentage of that slice (e.g. for 12 particles with 0.1 = 30 degrees +/- 1.5 degrees).")]
    private float directionVariancePercentage;

    [SerializeField]
    private float forceOnShrapnel;
    
    public void Blast()
    {
        var degreesPerItem = 360.0f / this.numberOfProjectiles;
        var degreesVariance = degreesPerItem * this.directionVariancePercentage;
        var halfDegreesVariance = degreesVariance / 2.0f;
        
        for(var i = 0; i < this.numberOfProjectiles; i++)
        {
            var angle = i * degreesPerItem + Random.Range(-halfDegreesVariance, halfDegreesVariance);
            var shrapnel = PoolingFactory.SpawnOrRecycle<Rigidbody2D>(this.shrapnelPrefab.transform, this.transform.position);

            ApplyForceFromblast(shrapnel, angle);
        }
        
        Debug.Log("Spawned " + this.numberOfProjectiles + " shrapnel");
    }

    private void ApplyForceFromblast(Rigidbody2D shrapnel, float angle)
    {
        var forceDirection = DegreeToVector2(angle);
        var forceDampener = Random.Range(0.7f, 1.0f);
        shrapnel.AddForce(forceDirection * forceDampener * this.forceOnShrapnel);
    }

    private static Vector2 RadianToVector2(float radian)
    {
        return new Vector2(Mathf.Cos(radian), Mathf.Sin(radian));
    }
  
    private static Vector2 DegreeToVector2(float degree)
    {
        return RadianToVector2(degree * Mathf.Deg2Rad);
    }
}