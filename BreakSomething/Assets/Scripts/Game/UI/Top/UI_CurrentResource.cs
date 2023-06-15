using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_CurrentResource : UIElement
{
    public TMP_Text coin;
    public TMP_Text dia;

    public override void SyncInit()
    {
        Observer.instance.resourceUpdateEvent.AddListener(UpdateResourceText);
    }

    private void UpdateResourceText()
    {
        coin.SetText(string.Format("{0:#,0}", StaticValue.PlayerInfo.coin));
        dia.SetText(string.Format("{0:#,0}", StaticValue.PlayerInfo.dia));
    }
}