using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    [SerializeField] protected float Health;
    public List<Action> deathCallbacks;
    
    // Start is called before the first frame update
    void Start()
    {
        deathCallbacks = new List<Action>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    //Applies damage and returns remaining health
    public virtual void ApplyDamage(float damage)
    {
        float damageToDo = damage;
        Health -= damageToDo;

        if (Health <= 0)
            onKill();
    }

    //Overload with optional callback functionality on death
    public virtual void ApplyDamage(float damage, Action callback)
    {
        float damageToDo = damage;
        Health -= damageToDo;

        if (Health <= 0)
        {
            callback();
            onKill();
        }
    }

    // Adds a callback function to the enemy
    public virtual void AddDeathCallback(Action callback)
    {
        deathCallbacks.Add(callback);
    }

    // Do all the death callbacks
    public virtual void onKill()
    {
        if(deathCallbacks.Count > 0)
        {
            foreach(Action callback in deathCallbacks)
            {
                callback();
            }
        }

        Destroy(this.gameObject);
    }
}
