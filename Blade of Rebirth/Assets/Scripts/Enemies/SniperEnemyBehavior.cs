using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperEnemyBehavior : EnemyBehavior
{


    // Update is called once per frame
    void Update()
    {
        // Check for player position
        inSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        inAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (isUninterruptable)
        {
            state = AIState.forceMove;
            UninterruptableMove();
        }
        else
        {
            if (inSightRange)
            {
                state = AIState.attack;
                AttackPlayer();
            }
        }
    }

    // Try to attack the player if in range
    protected override void AttackPlayer()
    {
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            // Attack the player
            print("Attaking");

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
}
