using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This Script only contains what each house does for itself.
// A main script will query the variables to do greater calculations.

public class HouseBase : MonoBehaviour
{
    //Active hours, Power consuption per hour (day/night)
    //For active hours, 0 is midnight, 23 is 11 pm. This is multiplied by the ticks per day and stuff for calculations.
    [SerializeField] public float activeHoursStart;
    [SerializeField] public float activeHoursEnd;
    [SerializeField] public float extraActiveHour;
    [SerializeField] public float powerConsumptionActive;
    [SerializeField] public float powerConsumptionInactive;

    //STATICS: DO NOT CHANGE WITH CODE. READ ONLY.
    private float TICK = 10.0f;
    private float TICK_PER_HOUR = 24.0f;
    private float powerConsumptionMin = 7.0f;
    private float powerConsumptionMax = 40.0f;

    // Gets written to.
    public float currentPowerConsumption;

    // boolean for management.
    public bool isBuisness = false;//Changes generation from random to set.

    // Start is called before the first frame update
    void Start()
    {
        if (!isBuisness)
        {
            //Randomise active hours start.
            Random.InitState(System.DateTime.Now.Millisecond);
            activeHoursStart = (float)(Random.Range(5, 11));

            //Randomise a time and set start + time as the end.
            activeHoursEnd = activeHoursStart + (float)(Random.Range(15, 18));
            if (activeHoursEnd > 24.0f)
            {
                activeHoursEnd = activeHoursEnd % 24;
            }

            //Randomise an extra active hour
            extraActiveHour = (float)(Random.Range(0, 24));

            //Randomise power consumptions
            powerConsumptionActive = Random.Range(powerConsumptionMin, powerConsumptionMax);
            powerConsumptionInactive = Random.Range(0.1f, powerConsumptionMin);
        }
        else
        {
            activeHoursStart = 9.0f;
            activeHoursEnd = 6.0f;
            powerConsumptionActive = powerConsumptionMax * 10;
            powerConsumptionInactive = powerConsumptionMin * 10;
            extraActiveHour = 8.0f;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
       // check time
       // adjust consumption accordingly
    }


}
