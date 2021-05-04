using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExecuteEnemy : EventManager
{
    [SerializeField] GameObject target;
    [SerializeField] Transform spawnLocation;


    // When the node is activated in the chain of events
    void Awake()
    {
        output = GameObject.Find("Canvas");
        output.GetComponent<UIScript>().targetObject = target;
        output.GetComponent<UIScript>().waypointActive = true;
        output.GetComponent<UIScript>().currentObjective = CurrentObjective;
        target = GameObject.Instantiate(target, spawnLocation);
    }

    private void Update()
    {
        if(target != null)
        {
            // do nothing
        }
        else
        {
            Complete();
        }
    }
}
