using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class BombTrigger : MonoBehaviour, IRecyclable
{
    [SerializeField]
    private Explosion explosionPrefab;

    private bool exploded;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(this.exploded)
        {
            return;
        }
        
        Debug.Log("Explosion triggered");
        var explosion = PoolingFactory.SpawnOrRecycle(this.explosionPrefab.transform, this.transform.position);

        this.gameObject.SetActive(false);
        this.exploded = true;
    }

    public void Recycle()
    {
        this.exploded = false;
    }
}