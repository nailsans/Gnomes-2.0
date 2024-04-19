using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class EntityMove : MonoBehaviour
{
    // A script applied on entity that allows to implement movement for it
    private NavMeshAgent agent;
    private SphereCollider attackArea;
    private Transform currentTarget;
    public bool targetIsClose; //boolean shows if cur target is in attack area
    Animator animator;

    public LayerMask whatIsGround, whatIsPlayer;

    private EntityAttackArea entityAttackArea;

    private void Update()
    {
        // Finding closest targing
        currentTarget = findTarget();

        // Checking if target is in field of attack
        if (targetIsClose)
            // If target can be attacked, stop moving
            agent.isStopped = true;
        else
        {
            agent.isStopped = false;
            moveTowards(currentTarget);
        }
    }

    private void Awake()
    {
        // Getting components
        entityAttackArea = GetComponent<EntityAttackArea>();
        animator = GetComponentInChildren<Animator>();
        targetIsClose = false;
        attackArea = GetComponentInChildren<SphereCollider>();
        agent = GetComponent<NavMeshAgent>();
    }

    protected abstract Transform findTarget();

    /// <summary>
    /// Setting destination for movement AI
    /// </summary>
    /// <param name="target"></param>
    public void moveTowards(Transform target)
    {
        agent.SetDestination(target.position);
    }
}
