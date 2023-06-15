using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class UI_AnimatedText : MonoBehaviour, IPoolable
{
    public TMP_Text textComponent;

    protected const float MOVE_DISTANCE = 5.0f; // 위로 움직일 거리
    protected const float FADE_DURATION = 0.5f; // 페이드 지속 시간
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
            //위로 이동
            float yOffset = Mathf.Lerp(0f, MOVE_DISTANCE, elapsedTime / FADE_DURATION);
            GetComponent<RectTransform>().localPosition = new Vector3(0f, yOffset, 0f);

            //투명도 조절
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