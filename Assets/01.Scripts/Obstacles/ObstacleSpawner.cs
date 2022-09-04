using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class ObstacleSpawner : MonoBehaviour
{
    public MissileShooter missileST;

    [Space]
    [Title("����")]
    [FoldoutGroup("��ֹ� ��ȯ ����")]
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
