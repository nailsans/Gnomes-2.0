using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityAttackArea : MonoBehaviour
{
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
