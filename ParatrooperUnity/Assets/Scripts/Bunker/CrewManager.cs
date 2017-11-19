using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CrewManager : MonoBehaviour
{
    public int CrewCount => crewMembers.Count;
    public float HitPoints => hitPoints;
    public float MaxHitPoints => maxHitPoints;
    public float HealthPercentage => hitPoints / maxHitPoints;

    [SerializeField] [Tooltip("Root transform of the left guard structure.")] private Guard GuardLeft;
    [SerializeField] [Tooltip("Root transform of the right guard structure.")] private Guard GuardRight;
    [SerializeField] [Tooltip("Melee dust cloud.")] private SpriteRenderer MeleeCloud;
    [SerializeField] [Tooltip("Melee duration in seconds for a fully health attacker.")] private float MeleeDuration;

    [SerializeField] private Crew CommanderPrefab;
    [SerializeField] private Crew GunnerPrefab;
    [SerializeField] private Crew GuardLeftPrefab;
    [SerializeField] private Crew GuardRightPrefab;

    private Transform crewParent;
    private List<Crew> crewMembers = new List<Crew>(10);
    private Dictionary<Role, int> crewCounts = new Dictionary<Role, int>(10);

    private Dictionary<Rank, List<Crew>> damageGroups =
        new Dictionary<Rank, List<Crew>>(Enum.GetValues(typeof(Rank)).Length);

    private float hitPoints = 0.0f;
    private float maxHitPoints = 0.0f;
    private int meleesInProgress = 0;

    void Awake()
    {
        crewParent = transform.Find("Crew");
        MeleeCloud.gameObject.SetActive(false);

        foreach (Rank rank in Enum.GetValues(typeof(Rank)))
        {
            damageGroups.Add(rank, new List<Crew>());
        }

        foreach (Role role in Enum.GetValues(typeof(Role)))
        {
            crewCounts.Add(role, 0);
        }

        AddCommander();
        AddGunner();
    }

    void Update()
    {
    }

    private bool AddMember(Crew prefab)
    {
        Crew member = Instantiate(prefab, crewParent);

        crewMembers.Add(member);
        crewCounts[member.Role]++;
        damageGroups[member.Rank].Add(member);

        maxHitPoints += member.MaxHitPoints;
        hitPoints += member.HitPoints;

        member.OnDeath += OnMemberDeath;
        member.OnDamage += OnMemberDamage;

        return true;
    }

    private void OnMemberDamage(Crew crew, float damage)
    {
        hitPoints -= damage;
    }

    private bool AddMemberWithLimit(Crew prefab, int limit)
    {
        if (crewCounts[prefab.Role] >= limit)
        {
            Debug.LogWarning("Already more than " + (limit) + " " + Enum.GetName(typeof(Role), prefab.Role) + "...");
            return false;
        }

        AddMember(prefab);
        return true;
    }

    private void OnMemberDeath(Crew crew)
    {
        crewMembers.Remove(crew);
        crewCounts[crew.Role]--;
        damageGroups[crew.Rank].Remove(crew);

        if (crew.Role == Role.Commander)
        {
            Debug.Log("GAME OVER!!!");
        }
        else if (crew.Role == Role.Gunner)
        {
            Debug.Log("TODO: Gunner Dead - shoot slower!");
        }
        else if (crew.Role == Role.GuardLeft)
        {
            GuardLeft.Disable();
        }
        else if (crew.Role == Role.GuardRight)
        {
            GuardRight.Disable();
        }
    }

    public void AddCommander()
    {
        AddMemberWithLimit(CommanderPrefab, 1);
    }

    public void AddGunner()
    {
        AddMemberWithLimit(GunnerPrefab, 1);
    }

    public void AddGuardLeft()
    {
        var added = AddMemberWithLimit(GuardLeftPrefab, 1);

        if (added)
        {
            GuardLeft.Enable();
        }
    }

    public void AddGuardRight()
    {
        var added = AddMemberWithLimit(GuardRightPrefab, 1);

        if (added)
        {
            GuardRight.Enable();
        }
    }

    public void DefendAgainstMeleeAttack(float attackerHealthPercentage, float attackerDamage)
    {
        meleesInProgress++;

        MeleeCloud.gameObject.SetActive(true);

        StartCoroutine(ResolveMeleeAttack(attackerHealthPercentage, attackerDamage * attackerHealthPercentage));
    }

    IEnumerator ResolveMeleeAttack(float attackerHealthPercentage, float attackerDamage)
    {
        var duration = MeleeDuration;
        while (duration > 0.0f)
        {
            duration -= Time.deltaTime;

            yield return null;
        }

        TakeDamage(attackerDamage * attackerHealthPercentage);

        meleesInProgress--;

        if (meleesInProgress == 0)
        {
            MeleeCloud.gameObject.SetActive(false);
        }
    }

    private void TakeDamage(float damage)
    {
        foreach (Rank rank in Enum.GetValues(typeof(Rank)))
        {
            var ofRank = damageGroups[rank];

            if (ofRank.Count == 0)
            {
                continue;
            }


            ofRank[(int) Random.Range(0, ofRank.Count)].Damage(damage);
            return;
        }
    }
}