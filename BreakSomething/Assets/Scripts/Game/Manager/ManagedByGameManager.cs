using UnityEngine;

public abstract class ManagedByGameManager : MonoBehaviour
{
    public abstract void OnAsyncInit();
    public abstract void OnSyncInit();
    public abstract void OnClear();
}