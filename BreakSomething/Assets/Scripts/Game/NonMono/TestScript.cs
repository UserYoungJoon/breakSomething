using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TestScript : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        // Handle Ŭ�� �� ������ �ڵ� �ۼ�
        Debug.Log("Handle Clicked!");
    }
}
