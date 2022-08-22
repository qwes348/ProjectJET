using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class Poolable : MonoBehaviour
{    
    // 풀링에 사용할 ID
    [SerializeField]
    private string id;
    // 시작할 때 생성할 갯수
    public int instantateCount;

    // 자동으로 푸쉬할지 여부
    [Space]
    public bool isAutoPush;
    // 자동푸쉬 타임
    [ShowIf("isAutoPush")]
    public float autoPushTime;

    private float currentTime;

    #region 프로퍼티
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
