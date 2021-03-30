using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Parent class for more intricate Event Managers or custom events
/// </summary>
public class EventManager : MonoBehaviour
{
    [SerializeField] protected GameObject output;
    [SerializeField] protected EncounterManager manager;
    [SerializeField] protected string CurrentObjective = "Survive";

    // Start is called before the first frame update
    void Start()
    {
        // Start inactive and wait for the encounter manager to activate
        // gameObject.SetActive(false);
    }

    // Gets called upon completion of the objective to determine if the player can continue
    public virtual void Complete()
    {
        print("Complete");
        // Activate the next event when complete
        manager.ActivateNextObject();
    }
}
