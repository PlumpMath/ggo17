using UnityEngine;

[RequireComponent(typeof(Pooled))]
public class BombTrigger : MonoBehaviour, IPooledOnDestroy
{
    [SerializeField]
    private ShrapnelBlast shrapnelBlastPrefab;

    [SerializeField]
    private Shockwave shockwavePrefab;

    [SerializeField]
    private bool DebugOn = false;
    
    private Pooled pooled;

    private Vector3 shrapnelSpawnPoint;

    private void Awake()
    {
        this.pooled = this.GetComponent<Pooled>();
    }

    public void OnDestroyPooled()
    {
        if(this.shrapnelBlastPrefab != null)
        {
            if (DebugOn) Debug.Log("Spawned shrapnel");
            PoolingFactory.SpawnOrRecycle<ShrapnelBlast>(this.shrapnelBlastPrefab.transform, this.shrapnelSpawnPoint).Blast();

        }

        if (this.shockwavePrefab != null)
            PoolingFactory.SpawnOrRecycle(this.shockwavePrefab.transform, this.transform.position);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        var closestPoint = other.collider.bounds.ClosestPoint(transform.position);
        this.shrapnelSpawnPoint = closestPoint + (closestPoint - other.transform.position).normalized * 0.1f; // Try not to explode inside the target!

        if(other.transform.name == "Ground")
        {
            this.shrapnelSpawnPoint = this.shrapnelSpawnPoint + new Vector3(0, 0.1f, 0);
        }
        if (DebugOn) Debug.Log("ShrapnelSpawn: " + this.shrapnelSpawnPoint);
        
        this.pooled.DestroyPooled();
    }
}