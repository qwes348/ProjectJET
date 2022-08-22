using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class PoolManager : SerializedMonoBehaviour
{
    public static PoolManager Instance
    {
        get
        {
            return instance;
        }
    }
    private static PoolManager instance;

    // 딕셔너리를 채우기위한 풀링할 모든 오브젝트들
    public List<Poolable> allPoolables;

    // 현재 나와있는(사용중인) 풀러블 오브젝트들
    private List<Poolable> currentActivePoolables;
    // 재고
    private Dictionary<string, Stack<Poolable>> poolDict;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        currentActivePoolables = new List<Poolable>();
        poolDict = new Dictionary<string, Stack<Poolable>>();

        // 풀러블 오브젝트 생성
        foreach(var p in allPoolables)
        {
            if(p.instantateCount > 0)
            {
                poolDict.Add(p.ID, new Stack<Poolable>());
                for (int i = 0; i < p.instantateCount; i++)
                {
                    var pool = Instantiate(p, transform);
                    pool.gameObject.SetActive(false);                    
                    poolDict[p.ID].Push(pool);
                }
            }
        }
    }

    public void Push(Poolable poolObj)
    {
        poolObj.gameObject.SetActive(false);

        // 풀러블 딕셔너리에 존재한다면
        if(poolDict.ContainsKey(poolObj.ID))
        {
            poolDict[poolObj.ID].Push(poolObj);
        }
        // 존재하지 않는다면 스택을 새로 생성해서 넣어줌
        else
        {
            poolDict.Add(poolObj.ID, new Stack<Poolable>());
            poolDict[poolObj.ID].Push(poolObj);
        }

        if (currentActivePoolables.Contains(poolObj))
            currentActivePoolables.Remove(poolObj);
    }

    /// <summary>
    /// 파라미터로 프리팹을 받는 Pop
    /// </summary>
    /// <param name="poolObj"> 프리팹 </param>
    /// <returns></returns>
    public Poolable Pop(Poolable poolObj)
    {
        // 풀러블 딕셔너리에 존재한다면
        if(poolDict.ContainsKey(poolObj.ID))
        {
            // 재고가 있다면 리턴
            if(poolDict[poolObj.ID].Count > 0)
            {
                var popObj = poolDict[poolObj.ID].Pop();
                currentActivePoolables.Add(popObj);
                return popObj;
            }
            // 없다면 생성 후 리턴
            else
            {
                var newPoolObj = Instantiate(poolObj, transform);
                newPoolObj.gameObject.SetActive(false);
                currentActivePoolables.Add(newPoolObj);
                return newPoolObj;
            }
        }
        // 딕셔너리에 없는 풀러블이라면
        else
        {
            // 딕셔너리에 자리를 만들고
            poolDict.Add(poolObj.ID, new Stack<Poolable>());
            // 생성 후 리턴
            var newPoolObj = Instantiate(poolObj, transform);
            newPoolObj.gameObject.SetActive(false);
            currentActivePoolables.Add(newPoolObj);
            return newPoolObj;
        }
    }

    /// <summary>
    /// 파라미터로 ID를 받는 Pop
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Poolable Pop(string id)
    {
        if(poolDict.ContainsKey(id))
        {
            if(poolDict[id].Count > 0)
            {
                var poolObj = poolDict[id].Pop();
                currentActivePoolables.Add(poolObj);
                return poolObj;
            }
            // 재고가 부족하다면
            else
            {
                // 풀러블 프리팹 리스트에서 ID로 찾아봄
                var poolPrefab = allPoolables.Find((p) => p.ID == id);
                // 그래도 없으면 리턴 널
                if (poolPrefab == null)
                    return null;

                // 생성 후 리턴
                var newPoolObj = Instantiate(poolPrefab, transform);
                newPoolObj.gameObject.SetActive(false);
                currentActivePoolables.Add(newPoolObj);
                return newPoolObj;
            }
        }
        // 풀러블 딕셔너리에 없다면
        else
        {
            // 풀러블 프리팹 리스트에서 ID로 찾아봄
            var poolPrefab = allPoolables.Find((p) => p.ID == id);
            // 그래도 없으면 리턴 널
            if (poolPrefab == null)
                return null;

            // 딕셔너리에 자리를 만들고
            poolDict.Add(id, new Stack<Poolable>());
            // 생성 후 리턴
            var newPoolObj = Instantiate(poolPrefab, transform);
            newPoolObj.gameObject.SetActive(false);
            currentActivePoolables.Add(newPoolObj);
            return newPoolObj;
        }
    }
}
