using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_JumpBtn : UIElement
{
    public Image superIcon;
    public Slider superSlider;

    private const float MAX_GAGE    = 1.0f;
    private const float GAGE_ADDER  = 0.34f;
    private float superJumpGage;


    private void OnEnable()
    {
        superJumpGage = 0;
        superIcon.fillAmount = 0;
    }

    public void OnClick()
    {
        Player.instance.InputJump();

        if(Player.instance.state.collideStayGround)
        {
            superJumpGage += GAGE_ADDER;

            superIcon.fillAmount = superJumpGage;
            if (superJumpGage >= MAX_GAGE)
            {
                superSlider.gameObject.SetActive(true);
                this.gameObject.SetActive(false);
            }
        }
    }
}