using UnityEngine;

[RequireComponent(typeof(Health))]
public class InitHealth : MonoBehaviour
{

    [SerializeField]
    private BunkerManager bunkerManager;
    
    void Awake()
    {
        GetComponent<Health>().InitHitPoints(this.bunkerManager.HitPoints);
    }
}
