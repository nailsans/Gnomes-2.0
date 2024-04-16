using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class EntityMove : MonoBehaviour
{
    private NavMeshAgent agent;
    private SphereCollider attackArea;
    private Transform currentTarget;
    public bool targetIsClose;

    public LayerMask whatIsGround, whatIsPlayer;

    private void Update()
    {
        currentTarget = findTarget();
        if (!targetIsClose)
            moveTowards(currentTarget);
    }

    private void Awake()
    {
        targetIsClose = false;
        attackArea = GetComponentInChildren<SphereCollider>();
        agent = GetComponent<NavMeshAgent>();
    }

    private bool closeToTarget(Transform target)
    {
        Debug.Log(Vector3.Distance(target.position, attackArea.gameObject.transform.position));
        Debug.Log(attackArea.radius * 2 + 0.5);
        if (Vector3.Distance(target.position, attackArea.gameObject.transform.position) < (attackArea.radius * 2))
        {
            Debug.Log("CLOSE " + Vector3.Distance(target.position, attackArea.gameObject.transform.position));
            return true;

        }
        return false;
    }

    protected abstract Transform findTarget();

    public void moveTowards(Transform target)
    {
        agent.SetDestination(target.position);
    }
}
