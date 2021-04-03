using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToLocation : EventManager
{
    [SerializeField] List<GameObject> GoalTrigger;

    // Start is called before the first frame update
    void Start()
    {
        // set the trigger callback function to complete()
        foreach ( GameObject trigger in GoalTrigger)
            trigger.GetComponent<EncounterTrigger>().SetTriggerCallback(Complete);
    }

    // Set the current objective when activated
    void Awake()
    {
        output = GameObject.Find("Canvas");
        output.GetComponent<UIScript>().currentObjective = CurrentObjective;
    }

    public override void Complete()
    {
        base.Complete();
        gameObject.SetActive(false);
    }
}
