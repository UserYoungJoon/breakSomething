using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class UI_SpecialSliderBase : UIElement, IPointerUpHandler
{
    public GameObject swapper;
    public Slider slider;

    private void OnEnable()
    {
        slider.value = 0;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (slider.value <= 0.1)
        {//조금밖에 안 움직이고 땠다면 클릭한 것으로 인식
            OnClick();
        }
        else if (slider.value >= 1)
        {
            OnSliderCompleted();
            this.gameObject.SetActive(false);
            swapper.SetActive(true);
        }
        else
        {
            slider.value = 0;
        }
    }


    protected abstract void OnClick();
    protected abstract void OnSliderCompleted();
}