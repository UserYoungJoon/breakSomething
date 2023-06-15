using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class UI_AnimatedText : MonoBehaviour, IPoolable
{
    public TMP_Text textComponent;

    protected const float MOVE_DISTANCE = 5.0f; // ���� ������ �Ÿ�
    protected const float FADE_DURATION = 0.5f; // ���̵� ���� �ð�
    protected float startTime;

    public void OnReturnToPool() { }

    public void OnSpawnFromPool()
    {
        startTime = Time.time;
    }

    private void Update()
    {
        float elapsedTime = Time.time - startTime;
        if (elapsedTime < FADE_DURATION)
        {
            //���� �̵�
            float yOffset = Mathf.Lerp(0f, MOVE_DISTANCE, elapsedTime / FADE_DURATION);
            GetComponent<RectTransform>().localPosition = new Vector3(0f, yOffset, 0f);

            //���� ����
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / FADE_DURATION);
            Color textColor = textComponent.color;
            textColor.a = alpha;
            textComponent.color = textColor;
        }
        else
        {
            OnEnd();
        }
    }

    protected abstract void OnEnd();
}