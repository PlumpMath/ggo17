using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    private new Rigidbody2D rigidbody2D;

    private void Awake()
    {
        this.rigidbody2D = this.GetComponent<Rigidbody2D>();
    }

    public void AddForce(Vector2 force)
    {
        this.rigidbody2D.AddForce(force);
    }
}