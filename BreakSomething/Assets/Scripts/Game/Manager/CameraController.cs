using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : ManagedByGameManager
{
    public Transform cameraTransform; // 카메라의 Transform 컴포넌트
    private float shakeDuration = 0.2f; 
    private float shakeMagnitude = 0.15f; 
    private Vector3 originalPosition; // 원래 카메라 위치

    public override void OnAsyncInit()
    {
        originalPosition = transform.localPosition;
    }

    public override void OnSyncInit()
    {
        var observer = Observer.instance;
        observer.playerDamagedEvent.AddListener(OnPlayerDamaged);
        observer.playerUltEvent.AddListener(OnUltimate);
    }

    public override void OnClear() { }

    private void Update()
    {
        var player = Player.instance.transform.position;
        transform.localPosition = new Vector3(player.x, player.y + 2.5f, -10);
    }

    #region OnEvent
    public void OnPlayerDamaged()
    {
        StartCoroutine(DamagedEffect());
    }

    private IEnumerator DamagedEffect()
    {//Shake When damaged
        float elapsedTime = 0f;
        while (elapsedTime < shakeDuration)
        {
            Vector3 shakeVector = Random.insideUnitSphere * shakeMagnitude;
            cameraTransform.localPosition = originalPosition + shakeVector;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        cameraTransform.localPosition = originalPosition;
    }
    //***************** ↓ On Player Ultimate ↓ *******************//

    public void OnUltimate()
    {
        StartCoroutine(UltimateEffect());
    }

    private IEnumerator UltimateEffect()
    {
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(0.7f);

        Time.timeScale = 1;
        float elapsedTime = 0f;
        while (elapsedTime < shakeDuration)
        {
            Vector3 shakeVector = Random.insideUnitSphere * shakeMagnitude;
            cameraTransform.localPosition = originalPosition + shakeVector;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        cameraTransform.localPosition = originalPosition;
    }
    #endregion OnEvent
}