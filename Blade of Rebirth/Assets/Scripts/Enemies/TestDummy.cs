using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class TestDummy : EnemyBase
{
    [SerializeField] bool IsInvincible;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Applies damage and returns remaining health
    public override void ApplyDamage(float damage)
    {
        float damageToDo = damage;

        if (!IsInvincible)
            Health -= damageToDo;

        print("Damage Done: " + damageToDo);

        if (Health <= 0)
            onKill();
    }

    //Overload with optional callback functionality on death
    public override void ApplyDamage(float damage, Action callback)
    {
        float damageToDo = damage;

        if (!IsInvincible)
            Health -= damageToDo;

        print("Damage Done: " + damageToDo);

        if (Health <= 0)
        {
            callback();
            onKill();
        }
    }
}
