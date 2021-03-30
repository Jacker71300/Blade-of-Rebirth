using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToLocation : EventManager
{
    [SerializeField] GameObject GoalTrigger;

    // Start is called before the first frame update
    void Start()
    {
        // set the trigger callback function to complete()
        GoalTrigger.GetComponent<EncounterTrigger>().SetTriggerCallback(Complete);
    }

    // Set the current objective when activated
    void Awake()
    {
        print("Current Objective: " + CurrentObjective);
    }

    public override void Complete()
    {
        base.Complete();
        //GoalTrigger.SetActive(false);
        gameObject.SetActive(false);
    }
}
