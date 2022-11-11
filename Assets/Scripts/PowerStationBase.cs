using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class is to set the stats of and manage the power stations.
// The main script will query the stats of this script to calculate the things like electricity price

public class PowerStationBase : MonoBehaviour
{
    //MAKE SURE TIME MEASURED THINGS ARE IN THE SAME MAGNITUDE AS TICKS
    float TICK = 10.0f;

    [SerializeField] public string displayName;
    [SerializeField] public float outputPerTick;
    public float currentOutputPerTick;

    [SerializeField] public float startupTime; //Ticks taken to start up
    [SerializeField] public float outputDuration; // for things that can only output for x time before it runs out, like the pumped storage
    [SerializeField] private float cooldown; // The cooldown in ticks for how long it has to stay down.
    private float _cooldownTimer;
    [SerializeField] public float storageCapacity; // for when it makes too much and has to store it
    public float currentStorage; //How much power it has in excess.
    [SerializeField] public float CostToDeploy;
    [SerializeField] public float CostToRepair; //cost to fix station break event
    [SerializeField] public float priceOfElectricity;//this stations price of electricity
    public bool isStationOn = false;
    public bool isStationBroken = false;
    public bool isConnectedToGrid = false;
    public bool isAutoOnOff = false;

    private float timeInterval;
    private float powerOverTime;

    private float upTime;

    private bool _timeIntervalMet = false;

    // Start is called before the first frame update
    void Start()
    {
        _cooldownTimer = 0.0f;
        _timeIntervalMet = false;
        cooldown = cooldown * TICK;
        currentOutputPerTick = 0;
        currentStorage = 0;
        if (startupTime > 0){powerOverTime = outputPerTick / startupTime;}
        else { powerOverTime = outputPerTick; }
    }

    // Update is called once per frame
    void Update()
    {
        _timeIntervalMet = false;
        timeInterval += Time.deltaTime;
        // Check if the station is on and it is at the output.
        if (isStationOn)
        {
            if (timeInterval >= TICK)
            {

                if (currentOutputPerTick < outputPerTick)
                {
                    currentOutputPerTick = currentOutputPerTick + powerOverTime;
                    if (currentOutputPerTick > outputPerTick)
                    {
                        currentOutputPerTick = outputPerTick;
                    }

                }
                if (outputDuration != 0.0f)//output duration of 0 means that it is indefinate.
                {
                    // This is where limited tick outputs work.
                    upTime += Time.deltaTime;
                    if (upTime >= outputDuration)
                    {
                        currentOutputPerTick = 0.0f;
                        PowerOffStation();
                    }

                }
                // Add the output to the storage.
                currentStorage = currentStorage + currentOutputPerTick;
                // send power to grid.

                // clamp storage
                if (currentStorage >= storageCapacity)
                {
                    currentStorage = storageCapacity;
                }
                _timeIntervalMet = true;
            }
            
            //Anything else that needs to be done on tick:
        
        }

        //anything else needing the tick I can think of
        if(_timeIntervalMet)
        { timeInterval = 0.0f; }

        if(!isStationOn)
        {
            _cooldownTimer += Time.deltaTime;
        }
        if(isAutoOnOff)
        {
            if(_cooldownTimer>= cooldown)
            {
                PowerOnStation();
            }
        }
    }

    void PowerOnStation()
    {
        if(_cooldownTimer>= cooldown)
        {
            if(!isStationBroken)
            {
                // called when other script tells this to power the station on.
                isStationOn = true;
                timeInterval = 0.0f;
                upTime = 0.0f;
            }
        }
    }
    void PowerOffStation()
    {
        isStationOn = false;
        currentStorage = currentStorage + currentOutputPerTick;
        currentOutputPerTick = 0.0f;
        upTime = 0.0f;
        _cooldownTimer = 0.0f;
    }
    void BreakStation()
    {
        isStationBroken = true;
        isStationOn = false;
        currentOutputPerTick = 0.0f;
        currentStorage = 0.0f;
        upTime = 0.0f;
    }
    void RepairedStation()
    {
        isStationBroken = false;
        PowerOnStation();
    }
}
