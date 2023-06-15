using UnityEngine;

public abstract class UIElement : MonoBehaviour
{
    public virtual void AsyncInit() { }
    public virtual void SyncInit() { }
}