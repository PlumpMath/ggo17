using System;
using UnityEngine;

[RequireComponent(typeof(Pooled))]
public class FlakShellBombTrigger : MonoBehaviour, IPooledOnDestroy, IRecyclable
{
    [SerializeField]
    private Rigidbody2D shrapnelPrefab;
    
    [SerializeField]
    private ShrapnelBlast shrapnelBlastPrefab;

    [SerializeField]
    private Shockwave shockwavePrefab;

    [SerializeField, Range(0.0f, 5.0f)]
    private float fuseTime;

    [SerializeField]
    private bool DebugOn = false;

    private float time;

    private Pooled pooled;

    private void Awake()
    {
        this.pooled = this.GetComponent<Pooled>();
        this.Recycle();
    }

    private void Update()
    {
        this.time += Time.deltaTime;

        if(!(this.time >= this.fuseTime))
        {
            return;
        }
        
        this.pooled.DestroyPooled();
    }

    public void OnDestroyPooled()
    {
        if(this.shrapnelBlastPrefab != null)
        {
            if (DebugOn) Debug.Log("Spawned shrapnel");
            PoolingFactory.SpawnOrRecycle<ShrapnelBlast>(this.shrapnelBlastPrefab.transform, this.transform.position).Blast(this.shrapnelPrefab);

        }

        if(this.shockwavePrefab != null)
        {
            PoolingFactory.SpawnOrRecycle(this.shockwavePrefab.transform, this.transform.position);
        }
    }

    public void Recycle()
    {
        this.time = 0.0f;
    }
}
