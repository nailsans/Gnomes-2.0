using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHPSystem : MonoBehaviour
{
    [SerializeField] private int maxHp = 10;
    private int hp;
    [SerializeField] private int resistance = 0; /// <summary>
    /// процент нивелируемого урона
    /// </summary>

    private void Start()
    {
        hp = maxHp;
    }

    /// <summary>
    /// Taking damage
    /// </summary>
    /// <param name="damage"></param>
    public void takeDamage(int damage)
    {
        hp -= damage * (100 - resistance) / 100;
        if (hp <= 0) Death();
    }

    /// <summary>
    /// Executing death
    /// </summary>
    public void Death()
    {

    }
}
