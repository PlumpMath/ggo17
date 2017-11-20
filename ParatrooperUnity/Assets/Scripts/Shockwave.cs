using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class Shockwave : MonoBehaviour
{
    private CircleCollider2D blastRadius;
    
    private void Awake()
    {
        this.blastRadius = this.GetComponent<CircleCollider2D>();
        this.blastRadius.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        throw new System.NotImplementedException();
    }

    public void Trigger()
    {
        //put trigger code here
    }
}