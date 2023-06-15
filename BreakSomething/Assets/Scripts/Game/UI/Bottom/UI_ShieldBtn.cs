using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UI_ShieldBtn : UIElement, IPointerUpHandler, IPointerDownHandler
{
    public Button btn;
    public Image cooldownImg;

    private float currentCoolTime = 0.0f;

    private void Start()
    {
        Clear();
    }

    private void Clear()
    {
        cooldownImg.fillAmount = 0;
        btn.interactable = true;
    }

    void Update()
    {
        if (currentCoolTime > 0.0f)
        {
            currentCoolTime -= Time.deltaTime;
            cooldownImg.fillAmount -= 1.0f / (Player.ShieldCoolTime/Time.deltaTime);
            if(currentCoolTime < 0.0f)
            {
                Clear();
            }
        }
        else
        {
            btn.interactable = true;
        }
    }

    public void OnClick()
    {
        currentCoolTime = Player.ShieldCoolTime;
    }

    //방패로 튕겨 냈을 때, 버튼에서 손을 땠을 때 호출
    public void OnPointerUp(PointerEventData eventData)
    {//쿨타임 시작...
        if(btn.interactable)
        {
            currentCoolTime = Player.ShieldCoolTime;
            cooldownImg.fillAmount = 1;
            btn.interactable = false;
            Player.instance.SetShieldTo(false);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Player.instance.SetShieldTo(true);
    }
}