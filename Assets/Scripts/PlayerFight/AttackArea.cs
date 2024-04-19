using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    private int damage = 3;

    // WHen something is in field of view, we check if it is enemy and of so we attack it
    private void OnTriggerEnter(Collider other)
    {
        EntityDamagable enemy = other.GetComponent<EntityDamagable>();
        if (enemy != null)
        {
            enemy.takeDamage(damage);
        }
    }
}
