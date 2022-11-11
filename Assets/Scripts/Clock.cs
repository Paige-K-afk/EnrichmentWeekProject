using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour
{
    private float _timeElapsed;
    private float TICK = 10.0f;
    private int TICK_PER_DAY = 24;
    
    public float currentHour;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _timeElapsed += Time.deltaTime;

        int ticksElapsed = (int)(_timeElapsed / TICK);
        // calculate elapsed ticks.
        currentHour = (float)ticksElapsed;

        // if ticks are grater than ticks per day, reset counter.
        if(ticksElapsed >= TICK_PER_DAY)
        {
            _timeElapsed = 0.0f;
        }
    }
}
