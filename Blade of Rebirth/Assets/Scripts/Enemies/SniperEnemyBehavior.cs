using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SniperEnemyBehavior : EnemyBehavior
{
    [SerializeField] float windUpTime = 0.5f;
    float windUpTimeLeft;
    [SerializeField] float targettingTime = 3f;
    float targettingTimeLeft;

    Vector3 target;

    // Set basic values when instantiated
    private void Awake()
    {
        player = GameObject.Find("Main_Character").transform;
        agent = GetComponent<NavMeshAgent>();
        windUpTimeLeft = windUpTime;
        targettingTimeLeft = targettingTime;
    }

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

        if (!alreadyAttacked)
        {
            // Attack the player
            Attack();
        }
    }

    private void Attack()
    {
        // update target location if there is targetting time
        if (targettingTimeLeft > 0f)
        {
            target = player.transform.position;

            gameObject.GetComponent<LineRenderer>().SetPositions(new Vector3[] { transform.position, target });

            transform.LookAt(target);
            targettingTimeLeft -= Time.deltaTime;
        }
        else
        {
            targettingTimeLeft = 0f;

            if(windUpTimeLeft > 0f)
            {
                windUpTimeLeft -= Time.deltaTime;
            }
            else
            {
                windUpTimeLeft = 0f;
                RaycastHit hit;
                Vector3 direction = (target - transform.position).normalized;
                if (Physics.Raycast(transform.position, direction, out hit))
                {
                    if (hit.transform == player)
                    {
                        print("Attack Hit");
                    }
                    else
                    {
                        print("Attack Missed");
                    }
                }
                gameObject.GetComponent<LineRenderer>().SetPositions(new Vector3[] { transform.position, transform.position });
                Invoke(nameof(ResetAttack), timeBetweenAttacks);
            }
        }
    }

    protected override void ResetAttack()
    {
        alreadyAttacked = false;
        windUpTimeLeft = windUpTime;
        targettingTimeLeft = targettingTime;
    }


}
