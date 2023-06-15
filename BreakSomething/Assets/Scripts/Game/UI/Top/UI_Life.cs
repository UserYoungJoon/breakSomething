using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Life : UIElement
{
    public Sprite heart;
    public Sprite nullHeart;

    public override void SyncInit()
    {
        Observer.instance.playerDamagedEvent.AddListener(DecreaseLife);
    }

    private void DecreaseLife()
    {
        int life = BattleManager.instance.life;
        if (life > 1)
        {
            var currentheart = transform.GetChild(life).GetComponent<Image>();
            currentheart.sprite = nullHeart;
        }    
    }
}