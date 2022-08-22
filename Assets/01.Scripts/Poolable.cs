using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class Poolable : MonoBehaviour
{    
    // Ǯ���� ����� ID
    [SerializeField]
    private string id;
    // ������ �� ������ ����
    public int instantateCount;

    // �ڵ����� Ǫ������ ����
    [Space]
    public bool isAutoPush;
    // �ڵ�Ǫ�� Ÿ��
    [ShowIf("isAutoPush")]
    public float autoPushTime;

    private float currentTime;

    #region ������Ƽ
    public string ID => id;
    #endregion    

    private void OnEnable()
    {
        currentTime = 0f;
    }

    private void Update()
    {
        if(isAutoPush)
        {
            currentTime += Time.deltaTime;

            if (currentTime > autoPushTime)
                PoolManager.Instance.Push(this);
        }
    }
}
