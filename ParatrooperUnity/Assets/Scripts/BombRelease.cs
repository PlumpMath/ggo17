using System.Runtime.CompilerServices;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class BombRelease : MonoBehaviour, IRecyclable
{
    [SerializeField]
    private Rigidbody2D bombPrefab;
    [SerializeField]
    private Rigidbody2D releaseBody;
    [SerializeField]
    private float secondsUntillRelease;

    private SpriteRenderer bombPlaceholderRenderer;
    private float timeElapsed;
    private float randomTime;

    private float ActualSecondsUntillRelease => this.secondsUntillRelease + this.randomTime;

    private void Awake()
    {
        this.bombPlaceholderRenderer = this.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if(!this.bombPlaceholderRenderer.enabled)
        {
            return;
        }
        
        this.timeElapsed += Time.deltaTime;

        if(this.timeElapsed >= this.ActualSecondsUntillRelease)
        {
            this.DropBomb();            
        }
    }

    private void DropBomb()
    {
        var spawnedBomb = PoolingFactory.SpawnOrRecycle<Rigidbody2D>(this.bombPrefab.transform, this.transform.position);
        spawnedBomb.velocity = this.releaseBody.velocity;
        this.bombPlaceholderRenderer.enabled = false;
    }

    public void Recycle()
    {
        this.bombPlaceholderRenderer.enabled = true;
        this.timeElapsed = 0.0f;
        this.randomTime = Random.Range(-1.0f, 1.0f);
    }
}
