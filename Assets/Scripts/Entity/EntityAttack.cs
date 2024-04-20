using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityAttack : MonoBehaviour
{
    /// <summary>
    /// A script applied on entity that allows to implement attack for it
    /// </summary>
    [SerializeField] bool attacksPlayers = true;
    [SerializeField] bool attacksPeneks = false;
    [SerializeField] private int damage = 3;
    private GameObject model;
    Animator animator;
    private EntityMove _entityMove;

    [SerializeField] private GameObject attackArea = default(GameObject);
    private bool isAttacking = false;
    private float timeToAttack = 0.65f;

    /// <summary>
    /// Getting components
    /// </summary>
    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        _entityMove = GetComponent<EntityMove>();
        model = transform.Find("Model").gameObject;
    }

    /// <summary>
    /// Called when something is in sight of our attack field
    /// </summary>
    /// <param name="other"></param>
    public void startAttacking(Collider other)
    {
        /// If our entity is not attacking nothing, cancel attacking script
        if (!attacksPlayers) return;
        PlayerHPSystem playerHP = other.GetComponent<PlayerHPSystem>();
        if (playerHP == null) return;
        /// Making EntityMover stop moving as now we are going to attack
        _entityMove.targetIsClose = true;
        StartCoroutine(IAttackCycle(other));
    }

    public void StopAttacking()
    {
        _entityMove.targetIsClose = false;
        StopAllCoroutines();
    }

    /// <summary>
    /// Attacking target every time as cooldowm passes
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    private IEnumerator IAttackCycle(Collider other)
    {
        PlayerHPSystem playerHP = other.GetComponent<PlayerHPSystem>();
        while (true)
        {
            if (attacksPlayers)
            {
                if (playerHP != null)
                {
                    animator.SetTrigger("hit");
                    _entityMove.targetIsClose = true;
                    playerHP.takeDamage(damage);
                    
                }
            }

            yield return new WaitForSeconds(timeToAttack);
        }
    }
}
