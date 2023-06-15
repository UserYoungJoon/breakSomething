using UnityEngine;
using UnityEngine.UI;

public class UI_WaveSlider : UIElement
{
    public Slider slider;

    public override void SyncInit()
    {
        Observer.instance.waveClearEvent.AddListener(WaveUpdate);
    }

    private void WaveUpdate(int wave)
    {
        slider.value = (float)wave/BattleManager.instance.maxWave;
    }
}