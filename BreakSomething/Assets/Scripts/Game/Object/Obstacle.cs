using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour, IPoolable
{
    public ulong currentHP;
    public ulong maxHP;
    public int score;
    public int coin;

    public void Init(ulong _HP)
    {
        maxHP = _HP;
        currentHP = maxHP;
        score = (int)(maxHP * 3);
        coin = (int)(maxHP * 2);
    }

    public void OnSpawnFromPool()
    { 
        currentHP = maxHP;
        var rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(0, -1) * new Vector2(0, 0.8f);
    }

    public void OnReturnToPool(){}

    public void GetDamage(ulong _power, int _criticalChance, bool _isAbsoluteAttack)
    {
        //is Ultimate : 3น่
        //is SuperJump : 8น่
        ulong damage = 0;
        if (Random.Range(0, 100) <= _criticalChance)
        {
            damage = _power * 2;
        }
        else
        {
            damage = _power;
        }

        var observer = Observer.instance;
        if (_isAbsoluteAttack)
        {
            observer.breakEvent.Invoke(this);
            observer.resourceUpdateEvent.Invoke();
        }
        else
        {
            if (currentHP <= damage)
            {//Break
                observer.breakEvent.Invoke(this);
            }
            else
            {
                currentHP -= damage;
            }
        }
        observer.obstacleDamageEvent.Invoke(damage);
    }
}
