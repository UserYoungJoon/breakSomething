using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackEffect : MonoBehaviour, IPoolable
{
    public SpriteRenderer sprite;
    public BoxCollider2D boxCollider2D;

    
    public const float EFFECT_DURATION = 0.05f;
    private const float MAX_SCALE_X = 0.9f;
    private const float MAX_SCALE_Y = 1.5f;
    private const float LIVE_DURATION = 0.3f;
    private const float MOVE_SPEED = 3f;
    private float currentTime = 0f;
    private Vector3 originalScale = new Vector3(0.5f, 0.75f, 0);
    private Color baseColor = Color.white;

    private bool isAbsoluteAttack = false;
    private bool isSuperAttack = false;
    private bool isUltAttack = false;

    public void OnReturnToPool()
    {
        sprite.flipX = false;
        transform.position = new Vector3(0, 0, 0);
    }

    public void OnSpawnFromPool()
    {
        transform.position = new Vector3(0, 0, 0);
        transform.localScale = originalScale;
        sprite.color = new Color(baseColor.r, baseColor.g, baseColor.b, 255);
        boxCollider2D.enabled = true;
        currentTime = 0f;
        isAbsoluteAttack = false;
    }

    public void Set(bool _isAbsolute, bool _isSuperJump, bool _isUltattck)
    {
        isAbsoluteAttack = _isAbsolute;
        isSuperAttack = _isSuperJump;
        isUltAttack = _isUltattck;
    }

    public void SetToAbsoluteAttack()
    {
        isAbsoluteAttack = true;
    }

    private void Update()
    {
        currentTime += Time.deltaTime; // 경과 시간 업데이트
        if (currentTime < LIVE_DURATION)
        {
            //decrease alpha
            float alpha = baseColor.a - (baseColor.a / LIVE_DURATION) * currentTime;
            sprite.color = new Color(baseColor.r, baseColor.g, baseColor.b, alpha);

            //scale bigger
            Vector3 scale = sprite.transform.localScale;
            float factor    = currentTime / LIVE_DURATION;
            float newScaleX = scale.x + (MAX_SCALE_X - scale.x) * factor;
            float newScaleY = scale.y + (MAX_SCALE_Y - scale.y) * factor;
            transform.localScale = new Vector3(newScaleX, newScaleY, scale.z);

            //Control effect live time 
            if (currentTime > EFFECT_DURATION)
            {
                boxCollider2D.enabled = false;
            }
        }
        else
        {
            //kill this AttackEffect object
            sprite.color = new Color(baseColor.r, baseColor.g, baseColor.b, 0f);
            SpawnManager.instance.attackEffectPool.ReturnObjectToPool(this.gameObject);
        }

        //move to [Left or Right], and Up
        if (sprite.flipX)
        {
            transform.Translate(Vector2.left * MOVE_SPEED * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector2.right * MOVE_SPEED * Time.deltaTime);
        }
        transform.Translate(Vector2.up * MOVE_SPEED * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Obstacle>(out var _obstacle))
        {
            var player = Player.instance;
            _obstacle.GetDamage(player.power, player.criticalChance, isAbsoluteAttack);
        }
    }
}
