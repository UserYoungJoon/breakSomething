using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class UI_WaveText : UIElement
{
    public TMP_Text waveText;

    public override void SyncInit()
    {
        Observer.instance.waveClearEvent.AddListener(foo);
    }

    public void foo(int _wave)
    {
        waveText.SetText(_wave.ToString());
    }
}