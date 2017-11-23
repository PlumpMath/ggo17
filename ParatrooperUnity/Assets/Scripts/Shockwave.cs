using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Pooled))]
public class Shockwave : Damage, IRecyclable
{

    [SerializeField]
    [Tooltip("Percentage of damage done at the maximum distance from the center of the explosion.")]
    protected float DropOff = 0.1f;

    [SerializeField]
    [Tooltip("Time to grow from 0 scale to 1 scale in seconds.")]
    protected float growSeconds = 0.5f;
    
    private float scale;
    private ISet<GameObject> damaged = new HashSet<GameObject>();
    
    protected override void Awake()
    {
        base.Awake();

        this.Recycle();
    }

    protected void Update()
    {
        if(this.scale < 1.0f)
        {
            this.scale += Time.deltaTime * (1.0f / this.growSeconds);

            if(this.scale > 1.0f)
            {
                this.scale = 1.0f;
            }
            
            this.transform.localScale = new Vector3(this.scale, this.scale, this.scale);
        }
        else
        {
            this.DestroySelf();
        }
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if(this.damaged.Contains(other.transform.root.gameObject)) return;

        var health = other.transform.root.GetComponent<Health>();
        if(health == null) return;
        
        var thisCollider = this.GetComponent<Collider2D>();
        var estimatedMaxDistance = (thisCollider.bounds.size.x + thisCollider.bounds.size.y) / 2.0f / Mathf.Max(this.scale, 0.001f) / 2.0f;
        var distance = (this.transform.position - other.bounds.ClosestPoint(this.transform.position)).magnitude;
        var distancePercentage = ((estimatedMaxDistance - distance) / estimatedMaxDistance);
        var dropOffDamage = (1.0f - this.DropOff) * Amount;
        var guaranteedDamage = Amount * this.DropOff;
        var dmg = distancePercentage * dropOffDamage + guaranteedDamage;
        Debug.Log("ShockwaveDamage: " + other.name + ": " + dmg);

        health.Damage(dmg, Source);
        this.damaged.Add(other.transform.root.gameObject);
    }

    public void Recycle()
    {
        this.scale = 0.0f;
        this.transform.localScale = new Vector3(0, 0, 0);
        this.damaged.Clear();
    }
}