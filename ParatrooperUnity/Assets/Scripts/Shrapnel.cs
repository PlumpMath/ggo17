using UnityEngine;

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