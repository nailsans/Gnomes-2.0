using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public interface IDamagable {
    public void takeDamage(int damage);

}

public class EntityDamagable : MonoBehaviour
{
    [SerializeField] private int health = 5;
    [SerializeField] private GameObject model;
    private Quaternion startRotation;
    private Vector3 startPos;

    Animator animator;

    private void Awake()
    {
        startRotation = model.transform.rotation;
        startPos = model.transform.localPosition;
        animator = GetComponentInChildren<Animator>();
    }

    public void takeDamage(int damage)
    {
        StartCoroutine(IPlayDamageAnimation());
        health -= damage;
        if (health <= 0)
            callDeath();
    }

    public void callDeath()
    {
        StartCoroutine(IPlayDeathAnimation());
    }

    private IEnumerator IPlayDamageAnimation()
    {
        model.transform.Rotate(new Vector3(10, 10, 10));
        yield return new WaitForSeconds(0.25f);
        model.transform.SetLocalPositionAndRotation(startPos, startRotation);
    }

    private IEnumerator IPlayDeathAnimation()
    {
        animator.SetTrigger("dead");
        
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }
}
