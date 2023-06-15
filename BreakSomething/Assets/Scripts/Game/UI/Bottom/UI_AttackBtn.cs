using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_AttackBtn : UIElement
{
    public Image ultIcon;
    public Slider ultSlider;

    public override void SyncInit()
    {
        Observer.instance.obstacleDamageEvent.AddListener(OnDamage);
    }

    private void OnEnable()
    {
        ultGage = 0;
        ultIcon.fillAmount = 0;
    }

    public void OnClick()
    {
        Player.instance.InputAttack(true);
    }

    private float ultGage = 0;
    private const float MAX_GAGE = 1.0f;
    private const float GAGE_ADDER = 0.11f;
    private void OnDamage(ulong _dmg)
    {
        ultGage += GAGE_ADDER;
        ultIcon.fillAmount = ultGage;
        if (ultGage >= MAX_GAGE)
        {
            ultSlider.gameObject.SetActive(true);
            this.gameObject.SetActive(false);
        }
    }
}