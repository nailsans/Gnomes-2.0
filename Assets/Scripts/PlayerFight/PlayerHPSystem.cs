using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHPSystem : MonoBehaviour
{
    [SerializeField] private int maxHp = 10;
    private int hp;
    [SerializeField] private int resistance = 0; //процент нивелируемого урона

    private void Start()
    {
        hp = maxHp;
    }

    // Taking damage
    public void takeDamage(int damage)
    {
        hp -= damage * (100 - resistance) / 100;
        if (hp <= 0) Death();
    }

    // Executing death
    public void Death()
    {

    }
}
