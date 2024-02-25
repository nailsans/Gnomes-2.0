using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable {
    public void takeDamage(int damage);

}

public abstract class Enemy : MonoBehaviour
{
    private int health = 5;
    private int damage = 0;

    protected void Init(int health, int damage)
    {
        this.health = health;
        this.damage = damage;
    }
    
    public void takeDamage(int damage)
    {
        Debug.Log("took damage");
        health -= damage;
        if (health <= 0)
            callDeath();
    }

    public void callDeath()
    {
        Destroy(gameObject);
    }
}
