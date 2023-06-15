using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : ManagedByGameManager
{
    public static BattleManager instance;
    public int currentWave = 0;
    public int maxWave = 0;
    public int life = 3;
    public int score = 0;
    public Transform obstacles;
    //public int minExp;//최소 지수 입력
    //public int maxExp;//최대 지수 입력
    //public int currentMinExp;
    //public int currentMaxExp;

    private Queue<Obstacle> currentObstacles;
    private List<ulong>[] waves;
    private float waveClearedTime = 0;
    private bool waveCleared = false;

    public override void OnAsyncInit()
    {
        instance = this;
        maxWave  = 15;
        waves    = new List<ulong>[maxWave];
        currentObstacles = new Queue<Obstacle>(15);

        //원래는 데이터 테이블로 가져오거나 랜덤 지수 선택 시스템을 사용하려 했으나 시간 관계상 불가능하여 직접 입력
        waves[ 0] = new List<ulong> { 4, 4, 4, 1, 1, 1, 1, 1, 1 };
        waves[ 1] = new List<ulong> { 1, 2, 1, 1, 1, 1, 1, 1, 1 };
        waves[ 2] = new List<ulong> { 1, 1, 4, 1, 1, 1, 1, 1, 1 };
        waves[ 3] = new List<ulong> { 1, 1, 1, 1, 8, 1, 1, 1, 1 };
        waves[ 4] = new List<ulong> { 1, 1, 1, 1, 1, 1, 1, 1, 1 };
        waves[ 5] = new List<ulong> { 1, 1, 2, 1, 1, 1, 1, 1, 1 };
        waves[ 6] = new List<ulong> { 1, 1, 1, 1, 1, 1, 1, 1, 1 };
        waves[ 7] = new List<ulong> { 1, 1, 1, 1, 1, 1, 1, 1, 1 };
        waves[ 8] = new List<ulong> { 1, 1, 1, 1, 1, 1, 1, 1, 1 };
        waves[ 9] = new List<ulong> { 1, 1, 1, 1, 1, 1, 1, 1, 1 };
        waves[10] = new List<ulong> { 1, 1, 1, 1, 1, 1, 1, 1, 1 };
        waves[11] = new List<ulong> { 1, 1, 1, 1, 1, 1, 4, 1, 1 };
        waves[12] = new List<ulong> { 1, 1, 1, 1, 1, 1, 1, 1, 1 };
        waves[13] = new List<ulong> { 1, 1, 1, 1, 1, 1, 1, 1, 1 };
        waves[14] = new List<ulong> { 1, 1, 1, 1, 1, 1, 1, 1, 1 };
    }

    public override void OnSyncInit()
    {
        var observer = Observer.instance;
        observer.breakEvent.AddListener(OnBreak);
        observer.playerDamagedEvent.AddListener(OnDamaged);
        GenerateNewWave();
    }

    public override void OnClear() {}

    public void GenerateNewWave()
    {
        List<ulong> HPs = waves[currentWave];
        for (int i = 0; i < HPs.Count; i++)
        {
            var obstacle = SpawnManager.instance.SpawnObstacle(new Vector2(obstacles.position.x, obstacles.position.y+ 2*i));
            obstacle.Init(HPs[i]);
            currentObstacles.Enqueue(obstacle);
        }
    }

    private void OnBreak(Obstacle _obstacle)
    {
        currentObstacles.Dequeue();
        score += _obstacle.score;
        if (currentObstacles.Count == 0)
        {
            currentWave++;
            Observer.instance.waveClearEvent.Invoke(currentWave);
            waveCleared = true;
            waveClearedTime = Time.time;
        }
        StaticValue.PlayerInfo.coin += _obstacle.coin;
    }

    private void OnDamaged()
    {
        life -= 1;
        if (life == 0)
        {
            //Time.timeScale = 0; <- 현재 후처리 부분이 전혀 없어 주석 처리
            StopAllCoroutines();
            Observer.instance.gameEndEvent.Invoke(false);
        }
    }

    private void Update()
    {
        if (Player.instance.state.collideStayGround && waveCleared && ((Time.time - waveClearedTime) >= 1.4f))
        {
            if (currentWave > maxWave)
            {
                Observer.instance.gameEndEvent.Invoke(true);
            }
            else
            {
                GenerateNewWave();
                waveCleared = false;
            }
        }
    }
}
