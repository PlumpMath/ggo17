using UnityEngine;

public class DestroyBullets : MonoBehaviour {
    private void OnTriggerEnter2D(Collider2D other)
    {
        var pooled = other.gameObject.GetComponent<Pooled>();
        var damage = other.gameObject.GetComponent<Damage>();
        var shockwave = other.gameObject.GetComponent<Shockwave>();

        if (pooled != null && damage != null && shockwave == null) // Probably a bullet
        {
            pooled.DestroyPooled();
        }
    }
}
