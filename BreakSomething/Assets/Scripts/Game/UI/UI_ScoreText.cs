
using UnityEngine;

public class UI_ScoreText : UI_AnimatedText
{
    protected override void OnEnd()
    {
        SpawnManager.instance.scoreTextPool.ReturnObjectToPool(this.gameObject);
    }
}