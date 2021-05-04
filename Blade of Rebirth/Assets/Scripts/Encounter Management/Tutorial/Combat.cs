using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : EventManager
{
    [SerializeField] List<SpawnNode> Spawners;
    bool hasCompleted = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // When the object is activated in the chain of events
    void Awake()
    {
        output = GameObject.Find("Canvas");
        output.GetComponent<UIScript>().currentObjective = CurrentObjective;
        foreach (SpawnNode spawner in Spawners)
        {
            spawner.gameObject.SetActive(true);
            spawner.Activate();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Check each spawner for the amount alive
        foreach (SpawnNode spawner in Spawners)
        {
            if (spawner.GetTotalAlive() <= 0)
            {
                if (!hasCompleted)
                {
                    Complete();
                }
            }
        }
    }

    // Activate the next object but don't disable this one
    public override void Complete()
    {
        base.Complete();
        hasCompleted = true;
        gameObject.SetActive(false);
    }
}
