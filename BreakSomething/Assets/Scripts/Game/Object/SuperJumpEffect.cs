using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperJumpEffect : MonoBehaviour
{
    public Player player;

    private const float ACTION_DURATION = 1.0f;
    private float currentTime = 0;

    private void OnEnable()
    {
        currentTime = 0;
    }

    private void Update()
    {
        currentTime += Time.deltaTime;
        if(currentTime >= ACTION_DURATION)
        {
            player.SwapWithSuperJump(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent<Obstacle>(out var _obstacle))
        {
            _obstacle.GetDamage(player.power*8, player.criticalChance, false);
            if(_obstacle.currentHP >= (player.power*8))
            {
                player.SwapWithSuperJump(false);
            }
        }
    }
}