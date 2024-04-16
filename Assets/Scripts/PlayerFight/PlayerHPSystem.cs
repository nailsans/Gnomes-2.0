using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHPSystem : MonoBehaviour
{
    [SerializeField] private int maxHp = 10;
    private int hp;
    [SerializeField] private int resistance = 0; //������� ������������� �����

    private void Start()
    {
        hp = maxHp;
    }

    public void takeDamage(int damage)
    {
        Debug.Log("����� ������� ����");
        hp -= damage * (100 - resistance) / 100;
        if (hp <= 0) Death();
    }

    public void Death()
    {
        Debug.Log("����� ����");
    }
}
