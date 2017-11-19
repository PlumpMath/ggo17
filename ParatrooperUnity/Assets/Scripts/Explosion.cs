using UnityEngine;

[RequireComponent(typeof(ShrapnelBlast))]
public class Explosion : MonoBehaviour, IRecyclable
{
    private ShrapnelBlast shrapnelBlast;
    
    private void Awake()
    {
        this.shrapnelBlast = this.GetComponent<ShrapnelBlast>();
    }

    public void Recycle()
    {
        this.shrapnelBlast.Trigger();
    }
}

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
        for(int i = 0; i < this.numberOfProjectiles; i++)
        {
            var shrapnel = PoolingFactory.SpawnOrRecycle<Shrapnel>(this.shrapnelPrefab.transform, this.transform.position);
            shrapnel.ApplyForceFromBlast(this.forceOnShrapnel);
        }
    }
}

[RequireComponent(typeof(Rigidbody2D))]
public class Shrapnel : MonoBehaviour, IRecyclable
{
    private Rigidbody2D body;

    private Vector2 forceDirection;
    private float forceDampener;

    private void Awake()
    {
        this.body = this.GetComponent<Rigidbody2D>();
    }

    public void ApplyForceFromBlast(float forceMagnitude)
    {
        this.body.AddForce(this.forceDirection * this.forceDampener * forceMagnitude);
    }

    public void Recycle()
    {
        this.forceDirection = Random.insideUnitCircle;
        this.forceDampener = Random.Range(0.7f, 1.0f);
    }
}

[RequireComponent(typeof(CircleCollider2D))]
public class Shockwave : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        throw new System.NotImplementedException();
    }
}