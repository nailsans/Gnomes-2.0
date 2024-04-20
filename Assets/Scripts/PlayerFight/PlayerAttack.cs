using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private GameObject attackArea = default(GameObject);
    private bool isAttacking = false;
    private float timeToAttack = 0.25f;

    private void Start()
    {
        isAttacking = false;
        attackArea.SetActive(isAttacking);
    }
    /// <summary>
    /// Executing attack on the object in field of attack
    /// </summary>
    public void Attack()
    {
        if (!isAttacking)
        {
            StartCoroutine(IAttack());
        }
    }

    /// <summary>
    /// Couroutine with cooldown for attack
    /// </summary>
    /// <returns></returns>
    private IEnumerator IAttack()
    {
        Debug.Log("ATTACK!");
        isAttacking = true;
        attackArea.SetActive(isAttacking);
        yield return new WaitForSeconds(timeToAttack);
        isAttacking = false;
        attackArea.SetActive(isAttacking);
    }
}
