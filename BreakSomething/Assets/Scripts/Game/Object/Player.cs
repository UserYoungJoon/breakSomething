using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;

    public Rigidbody2D rigidbody;
    public Weapon weapon;
    public Pet pet;
    public GameObject shieldCollider;
    public SuperJumpEffect superJumpEffect;
    public GameObject upperBody;
    public GameObject lowerBody;
    
    public ulong power;
    public int powerMultiplier;
    public int criticalChance;
    public const float ShieldCoolTime = 1.0f;
    public bool isShielding;

    private List<Buff> activeBuffs = new List<Buff>(20);
    private void Awake()
    {
        instance = this;
        power = weapon.power;
        criticalChance = 5;
    }

    public void ApplyBuff(Buff buff)
    {
        activeBuffs.Add(buff);
        powerMultiplier += buff.damageMultiplier;
        criticalChance += buff.criticalChanceBonus;
    }

    public void SetShieldTo(bool _option)
    {
        isShielding = _option;
        shieldCollider.gameObject.SetActive(_option);
    }

    public struct State
    {
        public bool collideStayGround;
    }
    public State state;

    #region Physics
    private void OnCollisionEnter2D(Collision2D _collision)
    {
        if (_collision.collider.TryGetComponent<Ground>(out var ground))
        {
            state.collideStayGround = true;

            //땅에 서있을 때는 Block에게 깔리도록
            Physics2D.IgnoreLayerCollision(
                StaticValue.PhysicsLayer.OBSTACLE,
                StaticValue.PhysicsLayer.PLAYER,
                true);
        }
    }

    private void OnCollisionExit2D(Collision2D _collision)
    {
        if (_collision.collider.TryGetComponent<Ground>(out var ground))
        {
            state.collideStayGround = false;

            //땅에서 떨어져있다면 Block과 충돌 가능하도록
            Physics2D.IgnoreLayerCollision(
                StaticValue.PhysicsLayer.OBSTACLE,
                StaticValue.PhysicsLayer.PLAYER,
                false);
        }
    }
    #endregion Physics

    #region Update
    private void Update()
    {
        UpdateBuffs();
        UpdateAttack();
        UpdateCollider();
    }

    private void UpdateBuffs()
    {

        for (int i = (activeBuffs.Count - 1); i >= 0; i--)
        {
            Buff buff = activeBuffs[i];
            buff.duration -= Time.deltaTime;
            if (buff.duration <= 0f)
            {
                powerMultiplier -= buff.damageMultiplier;
                criticalChance -= buff.criticalChanceBonus;
                activeBuffs.RemoveAt(i);
            }
        }
    }
    private bool attackable = true;
    private bool nowLeft = false;
    private float attackMotionTime = 0.2f; // can be increase or decrease by 
    private float currentAttackMotionTime = 0.0f;
    public Transform Left;
    public Transform Right;
    private Queue<bool> attackQueue = new Queue<bool>(10);

    private void UpdateAttack()
    {
        if (attackable)
        {
            //공격이 가능하고 공격 입력이 있었다면...
            if (attackQueue.Count > 0)
            {
                var attackEffect = SpawnManager.instance.SpawnAttackEffect(nowLeft ? Left : Right, nowLeft);
                nowLeft = !nowLeft;
                currentAttackMotionTime = attackMotionTime;
                attackEffect.Set(attackQueue.Dequeue(), false, false);
            }
        }
        else
        {
            currentAttackMotionTime -= Time.deltaTime;
            if ((currentAttackMotionTime <= 0) && (!isShielding))
            {
                attackable = true;
            }
        }

    }

    private void UpdateCollider()
    {
        if (state.collideStayGround)
        {

        }
    }
    #endregion Update

    #region Control

    private const float JUMP_POWER = 15f;
    public void InputJump()
    {
        if (state.collideStayGround)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, JUMP_POWER);
        }
    }

    public void InputSuperJump()
    {
        SwapWithSuperJump(true);
    }

    public void SwapWithSuperJump(bool _toSuperJump)
    {
        if(_toSuperJump)
        {
            rigidbody.AddForce(new Vector2(0, 50), ForceMode2D.Impulse);
        }

        superJumpEffect.gameObject.SetActive(_toSuperJump);
        upperBody.SetActive(!_toSuperJump);
        lowerBody.SetActive(!_toSuperJump);
        weapon.gameObject.SetActive(!_toSuperJump);
    }

    public void InputAttack(bool _isAbsolute)
    {
        attackQueue.Enqueue(_isAbsolute);
    }

    public void InputUltAttack()
    {
        attackQueue.Enqueue(true);
        Observer.instance.playerUltEvent.Invoke();
    }
    #endregion Control


    /// <summary>
    /// Abandoned class
    /// </summary>
    private class Animation
    {
        public enum UpperAnimation
        {
            Idle,
            Attack,
            Shield
        }
        public enum LowerAnimation
        {
            Jump,
            Idle,
            ReadyJump
        }
        public UpperAnimation upperAnimation;
        public LowerAnimation lowerAnimation;
    }
}
