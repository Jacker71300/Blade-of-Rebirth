using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    [SerializeField] protected float Health;
    
    // Start is called before the first frame update
    void Start()
    {
        
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

    public virtual void onKill()
    {
        Destroy(this.gameObject);
    }
}
