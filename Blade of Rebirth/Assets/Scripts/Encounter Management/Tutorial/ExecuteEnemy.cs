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
        target = GameObject.Instantiate(target, spawnLocation);
        target.GetComponent<EnemyBase>().AddDeathCallback(Complete);
    }
}
