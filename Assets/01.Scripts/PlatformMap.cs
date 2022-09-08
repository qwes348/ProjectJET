using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlatformMap : MonoBehaviour
{
    [SerializeField]
    private Poolable myPoolable;
    [SerializeField]
    private List<Transform> electricLinePoints;

    public UnityEvent onPushed;

    public void PushToPool()
    {
        if(myPoolable == null)
        {
            Destroy(gameObject);
            return;
        }

        onPushed?.Invoke();
        PoolManager.Instance.Push(myPoolable);
    }

    public Transform GetRandomElectricPoint()
    {
        return electricLinePoints[Random.Range(0, electricLinePoints.Count)];
    }
}
