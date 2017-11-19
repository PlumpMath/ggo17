using UnityEngine;

[RequireComponent(typeof(Health), typeof(Pooled))]
public class MeleeAttack : MonoBehaviour
{

    [SerializeField]
    private float Damage = 5.0f;
    
    private Health health;
    private Pooled pooled;
    
    void Awake()
    {
        health = GetComponent<Health>();
        pooled = GetComponent<Pooled>();
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player")) // Assume the bunker is the only object tagged as Player
        {
            var crewManager = other.gameObject.GetComponent<CrewManager>();
            Attack(crewManager);
        }
    }

    private void Attack(CrewManager crewManager)
    {
        pooled.DestroyPooled();

        crewManager.DefendAgainstMeleeAttack(health.Percentage, Damage);
    }
}