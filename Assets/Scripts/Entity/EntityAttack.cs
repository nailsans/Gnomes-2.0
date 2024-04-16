using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityAttack : MonoBehaviour
{
    [SerializeField] private GameObject attackArea = default(GameObject);
    private bool isAttacking = false;
    private float timeToAttack = 0.25f;

    private void Start()
    {
        isAttacking = false;
        attackArea.SetActive(isAttacking);
    }

    public void Attack()
    {
        StartCoroutine(IAttack());
    }

    private IEnumerator IAttack()
    {
        isAttacking = true;
        attackArea.SetActive(isAttacking);
        yield return new WaitForSeconds(timeToAttack);
        isAttacking = false;
        attackArea.SetActive(isAttacking);
    }
}
