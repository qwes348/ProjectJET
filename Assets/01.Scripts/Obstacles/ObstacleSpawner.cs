using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class ObstacleSpawner : MonoBehaviour
{
    public MissileShooter missileST;
    public Poolable electricLinePrefab;

    [Title("미사일")]
    [FoldoutGroup("장애물 소환 세팅")]
    [LabelText("미사일 소환 확률")]
    [PropertyRange(0, 1f)]
    public float missileSpawnRate;

    [Title("전기장")]
    [FoldoutGroup("장애물 소환 세팅")]
    [LabelText("전기장 소환 확률")]
    [PropertyRange(0, 1f)]
    public float electricLineSpawnRate;

    [FoldoutGroup("장애물 소환 세팅")]
    [LabelText("전기장 각도들")]
    public List<float> electricLineRotList;


    public void OnMapSpawned(Transform newMap)
    {
        var randomValue = Random.value;

        if(randomValue <= missileSpawnRate)
        {
            missileST.FireMissile();
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
}
