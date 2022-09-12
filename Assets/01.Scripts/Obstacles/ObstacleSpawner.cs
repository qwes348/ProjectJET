using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class ObstacleSpawner : MonoBehaviour
{
    public MissileShooter missileST;
    public Poolable electricLinePrefab;
    public Poolable lazerPrefab;

    #region �̻��� ����    
    [BoxGroup("��ֹ� ��ȯ ����")]
    [FoldoutGroup("��ֹ� ��ȯ ����/�̻���")]    
    [LabelText("�̻��� ��ȯ Ȯ��")]    
    [PropertyRange(0, 1f)]
    public float missileSpawnRate;

    [BoxGroup("��ֹ� ��ȯ ����")]
    [FoldoutGroup("��ֹ� ��ȯ ����/�̻���")]
    [LabelText("�̻��� �ִ� ���ӹ߻� ��")]
    public int maxMissileCount = 3;
    #endregion

    #region ������ ����
    [BoxGroup("��ֹ� ��ȯ ����")]
    [FoldoutGroup("��ֹ� ��ȯ ����/������")]
    [LabelText("������ ��ȯ Ȯ��")]
    [PropertyRange(0, 1f)]
    public float electricLineSpawnRate;

    [BoxGroup("��ֹ� ��ȯ ����")]
    [FoldoutGroup("��ֹ� ��ȯ ����/������")]
    [LabelText("������ ������")]
    public List<float> electricLineRotList;
    #endregion

    #region ������ ����
    [BoxGroup("��ֹ� ��ȯ ����")]
    [FoldoutGroup("��ֹ� ��ȯ ����/������")]    
    [LabelText("������ ��ȯ Ȯ��")]
    [PropertyRange(0, 1f)]
    public float lazerSpawnRate;

    [BoxGroup("��ֹ� ��ȯ ����")]
    [FoldoutGroup("��ֹ� ��ȯ ����/������")]
    [LabelText("������ ��ȯüũ ��")]
    public float lazerSpawnCheckTime;

    [BoxGroup("��ֹ� ��ȯ ����")]
    [FoldoutGroup("��ֹ� ��ȯ ����/������")]
    [LabelText("������ �ִ��ȯ ����")]
    public int lazerMaxCount;

    [FoldoutGroup("��ֹ� ��ȯ ����/������")]
    [LabelText("������ ��ȯ���� ���� ����(��)")]
    public float lazerUpgradeInterval;

    [FoldoutGroup("��ֹ� ��ȯ ����/������")]
    [LabelText("������ ���� �ּ�/�ִ밪")]
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
