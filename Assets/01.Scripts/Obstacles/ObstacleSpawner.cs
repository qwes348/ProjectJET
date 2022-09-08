using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class ObstacleSpawner : MonoBehaviour
{
    public MissileShooter missileST;
    public Poolable electricLinePrefab;

    [Title("�̻���")]
    [FoldoutGroup("��ֹ� ��ȯ ����")]
    [LabelText("�̻��� ��ȯ Ȯ��")]
    [PropertyRange(0, 1f)]
    public float missileSpawnRate;

    [Title("������")]
    [FoldoutGroup("��ֹ� ��ȯ ����")]
    [LabelText("������ ��ȯ Ȯ��")]
    [PropertyRange(0, 1f)]
    public float electricLineSpawnRate;

    [FoldoutGroup("��ֹ� ��ȯ ����")]
    [LabelText("������ ������")]
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
