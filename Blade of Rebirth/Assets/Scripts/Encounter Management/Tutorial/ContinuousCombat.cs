using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinuousCombat : EventManager
{
    [SerializeField] List<SpawnNode> Spawners;
    bool hasCompleted = false;

    // Start is called before the first frame update
    void Start()
    {
        // Do nothing
    }

    // When the object is activated in the chain of events
    void Awake()
    {
        print("Current Objective: " + CurrentObjective);
        foreach(SpawnNode spawner in Spawners)
        {
            spawner.gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Check each spawner for the amount alive
        foreach(SpawnNode spawner in Spawners)
        {
            if(spawner.GetTotalAlive() <= 0)
            {
                if (!hasCompleted)
                {
                    Complete();
                }
                spawner.ResetSpawns();
                spawner.Activate();
            }
        }
    }

    // Activate the next object but don't disable this one
    public override void Complete()
    {
        base.Complete();
        hasCompleted = true;
    }
}
