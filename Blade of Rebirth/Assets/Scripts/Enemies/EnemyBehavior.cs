using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehavior : MonoBehaviour
{
    public enum AIState {
        patrol = 0,
        alert = 1,
        attack = 2,
        forceMove = 3
    }

    [SerializeField] protected NavMeshAgent agent;
    [SerializeField] protected LayerMask whatIsGround, whatIsPlayer;

    // Vars for management
    public Transform player;
    public AIState state;
    //public EncounterManager manager;
    public GameObject manager;

    // Vars for patrolling
    public Vector3 walkPoint;
    public bool walkPointSet;
    public float walkPointRange;

    // Vars for attacking
    public float timeBetweenAttacks;
    protected bool alreadyAttacked;

    // Sight and attack
    public float sightRange, attackRange;
    public bool inSightRange, inAttackRange;

    protected bool isUninterruptable;

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

        if (isUninterruptable)
        {
            state = AIState.forceMove;
            UninterruptableMove();
        }
        else 
        {
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
    }

    protected virtual void UninterruptableMove()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }

    // Patrol the area
    protected virtual void Patrolling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
        
        
    }

    protected virtual void SearchWalkPoint()
    {
        // find a new point to wander to
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, whatIsGround))
            walkPointSet = true;
    }

    //Follow the player
    protected virtual void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    // Try to attack the player if in range
    protected virtual void AttackPlayer()
    {
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if(!alreadyAttacked)
        {
            // Attack the player
            print("Attaking");

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    // Reset the attack
    protected void ResetAttack()
    {
        alreadyAttacked = false;
    }

    // To set destinations from other classes
    public void SetDestination(Vector3 target)
    {
        agent.SetDestination(target);
    }

    // To set destinations from other classes and forces agent to get there before doing anything else
    public void SetUninterruptableDestination(Vector3 target)
    {
        walkPoint = target;
    }
}
