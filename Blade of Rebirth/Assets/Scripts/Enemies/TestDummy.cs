using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDummy : EnemyBase
{
    [SerializeField] bool IsInvincible;

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
