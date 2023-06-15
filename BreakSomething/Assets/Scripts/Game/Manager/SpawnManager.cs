using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpawnManager : ManagedByGameManager
{
    public static SpawnManager instance;
    public Pool attackEffectPool;
    public Pool obstaclePool;
    public Pool scoreTextPool;
    public Pool damageTextPool;
    public Transform obstacles;

    public override void OnAsyncInit()
    {
        instance = this;
        attackEffectPool.CreatePool();
        obstaclePool.CreatePool();
        scoreTextPool.CreatePool();
        damageTextPool.CreatePool();
    }

    public override void OnSyncInit()
    {
        Observer.instance.breakEvent.AddListener((obstacle) => { obstaclePool.ReturnObjectToPool(obstacle.gameObject); });
    }

    public override void OnClear() {}

    public AttackEffect SpawnAttackEffect(Transform _parent, bool _isLeft)
    {
        var effect = attackEffectPool.GetObjectFromPool().GetComponent<AttackEffect>();
        effect.GetComponent<AttackEffect>().sprite.flipX = _isLeft;

        var tmp = effect.transform.position;
        effect.transform.SetParent(_parent);
        effect.transform.localPosition = tmp;

        return effect;
    }

    public Obstacle SpawnObstacle(Vector2 _position, float _alpha = 1)
    {
        var obstacle = obstaclePool.GetObjectFromPool().GetComponent<Obstacle>();
        obstacle.transform.position = _position;
        Transform trans = obstacle.transform;
        Vector3 tmpPos = trans.position;
        trans.SetParent(obstacles);
        trans.localPosition = tmpPos;

        return obstacle;
    }

    public TMP_Text SpawnDamageText(ulong _dmg)
    {
        var damageText = damageTextPool.GetObjectFromPool().GetComponent<TMP_Text>();
        damageText.SetText(string.Format("{0:#,0}", _dmg));
        return damageText;
    }

    public TMP_Text SpawnScoreText(int _score)
    {
        var scoreText = scoreTextPool.GetObjectFromPool().GetComponent<TMP_Text>();
        scoreText.SetText($"+ {_score}");
        return scoreText;
    }
}