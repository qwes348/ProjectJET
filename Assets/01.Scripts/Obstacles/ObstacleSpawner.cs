using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class ObstacleSpawner : MonoBehaviour
{
    public MissileShooter missileST;

    [Space]
    [Title("세팅")]
    [FoldoutGroup("장애물 소환 세팅")]
    [PropertyRange(0, 1f)]
    public float missileSpawnRate;


    public void OnMapSpawned(Transform newMap)
    {
        var randomValue = Random.value;

        if(randomValue <= missileSpawnRate)
        {
            missileST.FireMissile();
        }
    }
}
