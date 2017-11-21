using UnityEngine;

[RequireComponent(typeof(Pooled))]
public class BombTrigger : MonoBehaviour, IPooledOnDestroy
{

    [SerializeField]
    private ShrapnelBlast shrapnelBlastPrefab;

    [SerializeField]
    private Shockwave shockwavePrefab;
    
    private Pooled pooled;

    public void Awake()
    {
        this.pooled = this.GetComponent<Pooled>();
    }

    public void OnDestroyPooled()
    {
        if (this.shrapnelBlastPrefab != null)
            PoolingFactory.SpawnOrRecycle(this.shrapnelBlastPrefab.transform, this.transform.position);
        
        if (this.shockwavePrefab != null)
            PoolingFactory.SpawnOrRecycle(this.shockwavePrefab.transform, this.transform.position);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        this.pooled.DestroyPooled();
    }
}