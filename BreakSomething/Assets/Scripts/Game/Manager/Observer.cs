using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Observer : ManagedByGameManager
{
    public static Observer instance;
    public UnityEvent<Obstacle> breakEvent;
    public UnityEvent resourceUpdateEvent;
    public UnityEvent<int> waveClearEvent;
    public UnityEvent<int> scorePlusEvent;
    public UnityEvent<ulong> obstacleDamageEvent;
    public UnityEvent playerDamagedEvent;
    public UnityEvent playerUltEvent;
    public UnityEvent<bool> gameEndEvent; //bool means game clear or over. clear = true

    public override void OnAsyncInit()
    {
        instance = this;
        breakEvent = new UnityEvent<Obstacle>();
        resourceUpdateEvent = new UnityEvent();
        waveClearEvent = new UnityEvent<int>();
        scorePlusEvent = new UnityEvent<int>();
        obstacleDamageEvent = new UnityEvent<ulong>();
        playerDamagedEvent = new UnityEvent();
        playerUltEvent = new UnityEvent();
        gameEndEvent = new UnityEvent<bool>();
    }

    public override void OnClear() {}

    public override void OnSyncInit() {}
}
