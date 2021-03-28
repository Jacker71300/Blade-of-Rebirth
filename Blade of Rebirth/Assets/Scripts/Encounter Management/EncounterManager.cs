using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class EncounterManager : MonoBehaviour
{
    //Get a list of all Event Managers in the world
    [SerializeField] List<GameObject> events;
    private int currentEvent = 0;

    // Start is called before the first frame update
    void Awake()
    {
        currentEvent = 0;
        ActivateNextObject();
    }

    // activate the next object in the queue
    public void ActivateNextObject()
    {
        if(currentEvent < events.Count)
        {
            events[currentEvent].SetActive(true);
            currentEvent++;
        }
    }

    // invoke the next object in the queue
    public void ActivateNextObject(float invokeTime)
    {
        Invoke(nameof(ActivateNextObject), invokeTime);
    }
}
