using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityAttackArea : MonoBehaviour
{
    [SerializeField] private int damage = 3;
    [SerializeField] bool attacksPlayers = true;
    [SerializeField] bool attacksPeneks = false;
    private EntityMove _entityMove;

    private void Awake()
    {
        _entityMove = GetComponentInParent<EntityMove>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (attacksPlayers)
        {
            PlayerHPSystem playerHP = other.GetComponent<PlayerHPSystem>();
            if (playerHP != null)
            {
                Debug.Log("true");
                _entityMove.targetIsClose = true;
                playerHP.takeDamage(damage);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (attacksPlayers)
        {
            PlayerHPSystem playerHP = other.GetComponent<PlayerHPSystem>();
            if (playerHP != null)
            {
                _entityMove.targetIsClose = false;
            }
        }
    }
}
