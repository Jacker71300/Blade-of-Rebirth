using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterTrigger : MonoBehaviour
{
    Action callback;

    // Only activates on player entry
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("PlayerObj"))
        {
            callback();
        }
    }

    // Sets the callback function
    public void SetTriggerCallback(Action callback)
    {
        this.callback = callback;
    }
}
