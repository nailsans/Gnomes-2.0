using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityAttackArea : MonoBehaviour
{
    /// <summary>
    /// A script for collider object of entity that has EntityAttack script, checks contacts with objects that can be attacked
    /// </summary>
    private EntityMove _entityMove;
    private EntityAttack _entityAttack;

    private void Awake()
    {
        ///Getting main components
        _entityMove = GetComponentInParent<EntityMove>();
        _entityAttack = GetComponentInParent<EntityAttack>();
    }

    private void OnTriggerEnter(Collider other)
    {
        /// When target is in attacking area, call the script for attacking
        _entityAttack.startAttacking(other);
    }

    private void OnTriggerExit(Collider other)
    {
        /// When target is out of area, stop attacking
        _entityAttack.StopAttacking();
    }

 
}
