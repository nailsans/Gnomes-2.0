using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    private int damage = 3;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Triggered");
        EntityDamagable enemy = other.GetComponent<EntityDamagable>();
        if (enemy != null)
        {
            enemy.takeDamage(damage);
        }
    }
}
