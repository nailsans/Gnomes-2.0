using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public interface IDamagable {
    public void takeDamage(int damage);

}

/// <summary>
/// Class for working with states of Entities that can be damaged 
/// </summary>
public class EntityDamagable : MonoBehaviour
{
    [SerializeField] private int health = 5;
    [SerializeField] private GameObject model;
    private Quaternion startRotation;
    private Vector3 startPos;

    Animator animator;

    /// <summary>
    /// Getting components
    /// </summary>
    private void Awake()
    {
        startRotation = model.transform.rotation;
        startPos = model.transform.localPosition;
        animator = GetComponentInChildren<Animator>();
    }

    /// <summary>
    /// Taking damaged is called by the thing that attack Entity
    /// </summary>
    /// <param name="damage"></param>
    public void takeDamage(int damage)
    {
        StartCoroutine(IPlayDamageAnimation());
        health -= damage;
        if (health <= 0)
            /// If no hp is left, execute death
            callDeath();
    }

    /// Death method
    public void callDeath()
    {
        StartCoroutine(IPlayDeathAnimation());
    }

    ///Plaiying damage animation
    private IEnumerator IPlayDamageAnimation()
    {
        model.transform.Rotate(new Vector3(10, 10, 10));
        yield return new WaitForSeconds(0.25f);
        model.transform.SetLocalPositionAndRotation(startPos, startRotation);
    }

    /// <summary>
    /// Animation corroutine
    /// </summary>
    /// <returns></returns>
    private IEnumerator IPlayDeathAnimation()
    {
        animator.SetTrigger("dead");
        
        ///After three seconds of animation being played, removing the object of dead entity
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }
}
