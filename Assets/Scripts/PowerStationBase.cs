using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class is to set the stats of and manage the power stations.
// The main script will query the stats of this script to calculate the things like electricity price

public class PowerStationBase : MonoBehaviour
{
    //MAKE SURE TIME MEASURED THINGS ARE IN THE SAME MAGNITUDE AS TICKS

    [SerializeField] public string displayName;
    [SerializeField] public float outputPerTick;
    public float currentOutputPerTick;

    [SerializeField] public float startupTime;
    [SerializeField] public float outputDuration; // for things that can only output for x time before it runs out, like the pumped storage
    [SerializeField] public float storageCapacity; // for when it makes too much and has to store it
    public float currentStorage; //How much power it has in excess.
    [SerializeField] public float CostToDeploy;
    [SerializeField] public float CostToRepair; //cost to fix station break event
    [SerializeField] public float priceOfElectricity;//this stations price of electricity
    public bool isStationOn = false;
    public bool isStationBroken = false;
    public bool isConnectedToGrid = false;

    private float timeInterval;
    private float powerOverTime;

    float TICK = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        currentOutputPerTick = 0;
        currentStorage = 0;
        powerOverTime = outputPerTick / startupTime;
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the station is on and it is at the output.
        if (isStationOn && currentOutputPerTick < outputPerTick)
        {
            timeInterval += Time.deltaTime;
            if (timeInterval >= TICK)
            {
                currentOutputPerTick = currentOutputPerTick + powerOverTime;
                if (currentOutputPerTick > outputPerTick)
                {
                    currentOutputPerTick = outputPerTick;
                }
                timeInterval = 0.0f;
            } 
        }


    }

    void PowerOnStation()
    {
        if(!isStationBroken)
        {
            // called when other script tells this to power the station on.
            isStationOn = true;
            timeInterval = 0.0f;
        }
    }
    void PowerOffStation()
    {
        isStationOn = false;
        currentStorage = currentStorage + currentOutputPerTick;
        currentOutputPerTick = 0.0f;
    }
    void BreakStation()
    {
        isStationBroken = true;
        isStationOn = false;
        currentOutputPerTick = 0.0f;
        currentStorage = 0.0f;
    }
    void RepairedStation()
    {
        isStationBroken = false;
        PowerOnStation();
    }
}
