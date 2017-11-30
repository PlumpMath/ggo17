using System.Globalization;
using Lanes;
using UnityEngine;

/*
 * Controls multiple lanes to ramp up difficulty the longer the player survives.
 */
public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private BunkerManager bunkerManager;

    [SerializeField]
    private LaneManager laneTop;

    [SerializeField]
    private LaneManager laneMiddle;

    [SerializeField]
    private LaneManager laneBottom;

    [SerializeField]
    private LaneManager laneLow;

    [SerializeField]
    private FlyHorizontal transport;

    [SerializeField]
    private FlyHorizontal bomber;

    private FlyHorizontal[] onlyTransports;
    private FlyHorizontal[] onlyBombers;
    private FlyHorizontal[] mixedPlanes;

    void Awake()
    {
        onlyTransports = new FlyHorizontal[] { this.transport };
        onlyBombers = new FlyHorizontal[] { this.bomber };
        mixedPlanes = new FlyHorizontal[] { this.transport, this.bomber };

        SetupLanes(this.bunkerManager.Day);
    }

    private void SetupLanes(int day)
    {
        this.SetupStartState();
        
        if(day >= 2)
        {
            SetState(this.laneTop, true, this.onlyTransports, 0.25f);
        }
        
        if(day >= 3)
        {
            SetState(this.laneTop, true, this.onlyTransports, 0.5f);
            SetState(this.laneMiddle, true, this.onlyTransports, 0.25f);
        }
        
        if(day >= 4)
        {
            SetState(this.laneTop, true, this.onlyTransports, 0.75f);
            SetState(this.laneMiddle, true, this.mixedPlanes, 0.25f);
        }
        
        if(day >= 5)
        {
            SetState(this.laneTop, true, this.mixedPlanes, 1.0f);
            SetState(this.laneMiddle, true, this.mixedPlanes, 0.5f);
        }

        if(day >= 8)
        {
            SetState(this.laneBottom, true, this.mixedPlanes, 0.5f);
        }

        if(day >= 10)
        {
            SetState(this.laneLow, true, this.mixedPlanes, 0.5f);
        }

        if(day >= 12)
        {
            SetState(this.laneTop, true, this.mixedPlanes, 1.0f);
            SetState(this.laneMiddle, true, this.mixedPlanes, 1.0f);
            SetState(this.laneBottom, true, this.mixedPlanes, 1.0f);
            SetState(this.laneLow, true, this.onlyBombers, 1.0f);
        }
    }

    private void SetupStartState()
    {
        SetState(this.laneTop, true, this.onlyTransports, 0.0f);
        SetState(this.laneMiddle, false, this.onlyTransports, 0.0f);
        SetState(this.laneBottom, false, this.onlyTransports, 0.0f);
        SetState(this.laneLow, false, this.onlyTransports, 0.0f);
    }

    private void SetState(LaneManager lane, bool autoSpawn, FlyHorizontal[] planes, float doubleSpawnChance)
    {
        lane.SetAutoSpawn(autoSpawn);
        lane.SetPlanes(planes);
        lane.SetDoubleSpawnChance(doubleSpawnChance);
    }
}
