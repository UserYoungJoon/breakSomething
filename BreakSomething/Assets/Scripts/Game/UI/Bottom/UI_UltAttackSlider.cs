using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_UltAttackSlider : UI_SpecialSliderBase
{
    protected override void OnClick()
    {
        Player.instance.InputAttack(false);
    }

    protected override void OnSliderCompleted()
    {
        Player.instance.InputUltAttack();
    }
}