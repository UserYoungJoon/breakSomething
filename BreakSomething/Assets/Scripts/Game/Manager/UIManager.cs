using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : ManagedByGameManager
{
    public static UIManager instance;
    public Transform canvas;
    public TMPro.TMP_Text totalScoreText;
    public Transform scoreTextParent;
    public Transform damageParent;
    public Image redImage;
    public Image ultimateBackground;


    public override void OnAsyncInit()
    {
        instance = this;

        foreach (var uiElement in canvas.GetComponentsInChildren<UIElement>())
        {
            uiElement.AsyncInit();
        }
    }

    public override void OnSyncInit()
    {
        var observer = Observer.instance;
        observer.breakEvent.AddListener(GenerateScoreText);
        observer.obstacleDamageEvent.AddListener(GenerateDamageText);
        observer.playerDamagedEvent.AddListener(StartRedScreenEffect);
        observer.playerUltEvent.AddListener(StartUltEffect);

        foreach (var uiElement in canvas.GetComponentsInChildren<UIElement>())
        {
            uiElement.SyncInit();
        }
    }

    public override void OnClear() { }

    public void GenerateScoreText(Obstacle _obstacle)
    {
        totalScoreText.SetText(string.Format("{0:#,0}", BattleManager.instance.score));
        var rect = SpawnManager.instance.SpawnScoreText(_obstacle.score).rectTransform;
        rect.SetParent(scoreTextParent, false);
        rect.localPosition = Vector3.zero;
    }

    public void GenerateDamageText(ulong _damage)
    {
        var rect = SpawnManager.instance.SpawnDamageText(_damage).rectTransform;
        rect.SetParent(damageParent, false);
        rect.localPosition = Vector3.zero;
    }

    //***************** бщ On Player Damaged бщ *******************//
    private const float DAMAGE_EFFECT_DURATION = 0.24f;
    private Color ORIGINAL_COLOR = new Color(1f, 0, 0, 0);
    private Color TRANSPARENT_COLOR = new Color(1f, 0, 0, 0.09f);

    public void StartRedScreenEffect()
    {
        StartCoroutine(DamagedEffect());
    }
    public IEnumerator DamagedEffect()
    {
        redImage.gameObject.SetActive(true);

        // alpha to 0.09
        float elapsedTime = 0f;
        while (elapsedTime < DAMAGE_EFFECT_DURATION)
        {
            float t = elapsedTime / DAMAGE_EFFECT_DURATION;
            redImage.color = Color.Lerp(ORIGINAL_COLOR, TRANSPARENT_COLOR, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // alpha to 0
        elapsedTime = 0f;
        while (elapsedTime < DAMAGE_EFFECT_DURATION)
        {
            float t = elapsedTime / DAMAGE_EFFECT_DURATION;
            redImage.color = Color.Lerp(TRANSPARENT_COLOR, ORIGINAL_COLOR, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        redImage.color = ORIGINAL_COLOR;
        redImage.gameObject.SetActive(false);
    }

    //***************** бщ On Ultmate Attack бщ *******************//
    public void StartUltEffect()
    {
        StartCoroutine(UltEffect());
    }
    public IEnumerator UltEffect()
    {
        ultimateBackground.gameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(0.5f);
        ultimateBackground.gameObject.SetActive(false);
    }

    public TMPro.TMP_Text asddassad;
    [ContextMenu("da1")]
    public void asd()
    {
        asddassad.transform.SetParent(damageParent, false);
    }
    [ContextMenu("da2")]
    public void asdgsd()
    {
        asddassad.transform.SetParent(scoreTextParent, false);
    }
}