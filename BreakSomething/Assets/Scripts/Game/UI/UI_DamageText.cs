
using UnityEngine;

public class UI_DamageText : UI_AnimatedText
{
    protected override void OnEnd()
    {
        SpawnManager.instance.damageTextPool.ReturnObjectToPool(this.gameObject);
    }
}