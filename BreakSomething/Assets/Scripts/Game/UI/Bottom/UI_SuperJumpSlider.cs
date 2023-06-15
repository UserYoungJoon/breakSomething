using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_SuperJumpSlider : UI_SpecialSliderBase
{
    protected override void OnClick()
    {
        Player.instance.InputJump();
    }

    protected override void OnSliderCompleted()
    {
        Player.instance.InputSuperJump();
    }
}