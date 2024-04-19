using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityAttackArea : MonoBehaviour
{
    // A script for collider object of entity that has EntityAttack script, checks contacts with objects that can be attacked
    private EntityMove _entityMove;
    private EntityAttack _entityAttack;

    private void Awake()
    {
        _entityMove = GetComponentInParent<EntityMove>();
        _entityAttack = GetComponentInParent<EntityAttack>();
    }

    private void OnTriggerEnter(Collider other)
    {
        _entityAttack.startAttacking(other);
    }

    private void OnTriggerExit(Collider other)
    {
        _entityAttack.StopAttacking();
    }

 
}
