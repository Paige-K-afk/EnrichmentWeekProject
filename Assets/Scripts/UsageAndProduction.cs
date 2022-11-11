using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UsageAndProduction : MonoBehaviour
{
    private GameObject[] arrayOfStations;
    private GameObject[] arrayOfHouses;
    private float powerConsumption;
    private float powerGeneration;
    private float powerStorage;

    [SerializeField] public TMP_Text powerConsumptionLabel;
    [SerializeField] public TMP_Text powerGenerationLabel;
    [SerializeField] public TMP_Text powerStorageLabel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetTotalPowerGenerated();
        GetTotalPowerConsumption();
        powerConsumptionLabel.text = powerConsumption.ToString();
        powerGenerationLabel.text = powerGeneration.ToString();
        powerStorageLabel.text = powerStorage.ToString();
    }

    void GetTotalPowerGenerated()
    {
        powerConsumption = 0.0f;
        powerGeneration = 0.0f;
        powerStorage = 0.0f;
        arrayOfStations = GameObject.FindGameObjectsWithTag("Power_Station");
        foreach (GameObject powerStation in arrayOfStations)
        {
            powerGeneration = powerGeneration + powerStation.GetComponent<PowerStationBase>().currentOutputPerTick;
            powerStorage = powerStorage + powerStation.GetComponent<PowerStationBase>().currentStorage;
            Debug.Log(arrayOfStations[0]);
        }
    }
    void GetTotalPowerConsumption()
    {
        powerConsumption = 0.0f;
        arrayOfHouses = GameObject.FindGameObjectsWithTag("House");
        foreach(GameObject house in arrayOfHouses)
        {
            powerConsumption = powerConsumption + house.GetComponent<HouseBase>().currentPowerConsumption;
            Debug.Log(arrayOfHouses[0]);
        }
    }
}
