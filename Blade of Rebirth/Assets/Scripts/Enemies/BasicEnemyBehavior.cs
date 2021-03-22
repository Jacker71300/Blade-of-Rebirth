using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class BasicEnemyBehavior : EnemyBehavior
{
    // Set basic values when instantiated
    void Awake()
    {
        player = GameObject.Find("Player Standin").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        // Check for player position
        inSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        inAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!inSightRange && !inAttackRange)
        {
            state = AIState.patrol;
            Patrolling();
        }
        if (inSightRange && !inAttackRange)
        {
            state = AIState.alert;
            ChasePlayer();
        }
        if (inSightRange && inAttackRange)
        {
            state = AIState.attack;
            AttackPlayer();
        }
    }

    protected override void AttackPlayer()
    {
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            // Attack the player
            Attack();

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    void Attack()
    {
        // Calculations to determine if player is going to be hit
        float hitPercent = 0.5f;

        float distanceModifier;

        float distance = Vector3.Distance(gameObject.transform.position, player.transform.position);

        // Calculate distance modifier
        if(distance >= 15f)
        {
            distanceModifier = 0.35f;
        }
        else if(distance >= 10f)
        {
            distanceModifier = 0.45f;
        }
        else if(distance >= 5f)
        {
            distanceModifier = 0.5f;
        }
        else
        {
            distanceModifier = 0.7f;
        }

        // Calculate health modifier
        float healthModifier;

        if (/*getplayerhealth*/ false)
            healthModifier = 0.4f;
        else
            healthModifier = 1f;

        hitPercent = hitPercent * distanceModifier * healthModifier;

        print(hitPercent);

        if (Random.Range(0f, 1f) <= hitPercent)
        {
            print("Attack Hit");
        }
        else
        {
            print("Attack Missed");
        }
    }
}
