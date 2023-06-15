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

    //���з� ƨ�� ���� ��, ��ư���� ���� ���� �� ȣ��
    public void OnPointerUp(PointerEventData eventData)
    {//��Ÿ�� ����...
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