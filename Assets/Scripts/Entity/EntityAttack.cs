using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityAttack : MonoBehaviour
{
    [SerializeField] bool attacksPlayers = true;
    [SerializeField] bool attacksPeneks = false;
    [SerializeField] private int damage = 3;
    private GameObject model;

    private EntityMove _entityMove;

    [SerializeField] private GameObject attackArea = default(GameObject);
    private bool isAttacking = false;
    private float timeToAttack = 2f;

    private void Awake()
    {
        _entityMove = GetComponent<EntityMove>();
        model = transform.Find("Model").gameObject;
    }

    public void startAttacking(Collider other)
    {
        if (!attacksPlayers) return;
        PlayerHPSystem playerHP = other.GetComponent<PlayerHPSystem>();
        if (playerHP == null) return;
        StartCoroutine(IAttackCycle(other));
    }

    public void StopAttacking()
    {
        _entityMove.targetIsClose = false;
        StopAllCoroutines();
    }

    private IEnumerator IAttackCycle(Collider other)
    {
        //if (!attacksPlayers) yield break;
        PlayerHPSystem playerHP = other.GetComponent<PlayerHPSystem>();
        //if (playerHP == null) yield break;
        while (true)
        {
            if (attacksPlayers)
            {
                if (playerHP != null)
                {
                    bettaAttackAnim();
                    _entityMove.targetIsClose = true;
                    playerHP.takeDamage(damage);
                }
            }

            yield return new WaitForSeconds(timeToAttack);
        }
    }

    private void bettaAttackAnim()
    {
        StartCoroutine(IAttack());
    }

    private IEnumerator IAttack()
    {
        Vector3 startPos = model.transform.localPosition;
        model.transform.localPosition = new Vector3(startPos.x, startPos.y + 1, startPos.z);
        yield return new WaitForSeconds(0.3f);
        model.transform.localPosition = startPos;
    }
}
