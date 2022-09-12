using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class ObstacleSpawner : MonoBehaviour
{
    public MissileShooter missileST;
    public Poolable electricLinePrefab;
    public Poolable lazerPrefab;

    #region 미사일 세팅    
    [BoxGroup("장애물 소환 세팅")]
    [FoldoutGroup("장애물 소환 세팅/미사일")]    
    [LabelText("미사일 소환 확률")]    
    [PropertyRange(0, 1f)]
    public float missileSpawnRate;

    [BoxGroup("장애물 소환 세팅")]
    [FoldoutGroup("장애물 소환 세팅/미사일")]
    [LabelText("미사일 최대 연속발사 수")]
    public int maxMissileCount = 3;
    #endregion

    #region 전기장 세팅
    [BoxGroup("장애물 소환 세팅")]
    [FoldoutGroup("장애물 소환 세팅/전기장")]
    [LabelText("전기장 소환 확률")]
    [PropertyRange(0, 1f)]
    public float electricLineSpawnRate;

    [BoxGroup("장애물 소환 세팅")]
    [FoldoutGroup("장애물 소환 세팅/전기장")]
    [LabelText("전기장 각도들")]
    public List<float> electricLineRotList;
    #endregion

    #region 레이저 세팅
    [BoxGroup("장애물 소환 세팅")]
    [FoldoutGroup("장애물 소환 세팅/레이저")]    
    [LabelText("레이저 소환 확률")]
    [PropertyRange(0, 1f)]
    public float lazerSpawnRate;

    [BoxGroup("장애물 소환 세팅")]
    [FoldoutGroup("장애물 소환 세팅/레이저")]
    [LabelText("레이저 소환체크 초")]
    public float lazerSpawnCheckTime;

    [BoxGroup("장애물 소환 세팅")]
    [FoldoutGroup("장애물 소환 세팅/레이저")]
    [LabelText("레이저 최대소환 갯수")]
    public int lazerMaxCount;

    [FoldoutGroup("장애물 소환 세팅/레이저")]
    [LabelText("레이저 소환갯수 증가 간격(초)")]
    public float lazerUpgradeInterval;

    [FoldoutGroup("장애물 소환 세팅/레이저")]
    [LabelText("레이저 높이 최소/최대값")]
    public Vector2 lazerHeightMinMax;
    #endregion

    private float lazerSpawnCheckTimer = 0f;

    private void Update()
    {
        if (GameManager.instance.GameState != Enums.GameState.Playing)
            return;

        if(lazerSpawnCheckTimer < lazerSpawnCheckTime)
        {
            lazerSpawnCheckTimer += Time.deltaTime;
            return;
        }

        lazerSpawnCheckTimer = 0f;
        var randomValue = Random.value;
        if(randomValue <= lazerSpawnRate)
        {
            //print("SpawnLazer");
            SpawnLazerObstacle();
        }
    }


    public void OnMapSpawned(Transform newMap)
    {
        var randomValue = Random.value;

        if(randomValue <= missileSpawnRate)
        {
            int fireCount;
            if (Time.time > 60f)
                fireCount = Random.Range(1, maxMissileCount + 1);
            else
                fireCount = 1;

            missileST.FireMissile(fireCount);
        }

        randomValue = Random.value;
        if(randomValue <= electricLineSpawnRate)
        {
            var electric = PoolManager.Instance.Pop(electricLinePrefab).GetComponent<ElectricLine>();
            electric.transform.SetParent(newMap);
            electric.transform.rotation = Quaternion.Euler(0f, 0f, electricLineRotList[Random.Range(0, electricLineRotList.Count)]);

            var platform = newMap.GetComponent<PlatformMap>();
            var point = platform.GetRandomElectricPoint();
            electric.transform.position = point.transform.position;

            electric.Init(newMap);            
        }
    }

    private void SpawnLazerObstacle()
    {
        int lazerCount = Mathf.Clamp(Mathf.FloorToInt(Time.time / lazerUpgradeInterval), 1, lazerMaxCount);

        for (int i = 0; i < lazerCount; i++)
        {
            var lazer = PoolManager.Instance.Pop(lazerPrefab).GetComponent<LazerObstacle>();
            lazer.transform.SetParent(transform);

            float lazerHeight = Random.Range(lazerHeightMinMax.x, lazerHeightMinMax.y);
            lazer.transform.localPosition = Vector3.up * lazerHeight;

            lazer.Init();
            lazer.Fire();
        }
    }
}
