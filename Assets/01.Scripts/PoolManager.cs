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

    // ��ųʸ��� ä������� Ǯ���� ��� ������Ʈ��
    public List<Poolable> allPoolables;

    // ���� �����ִ�(�������) Ǯ���� ������Ʈ��
    private List<Poolable> currentActivePoolables;
    // ���
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

        // Ǯ���� ������Ʈ ����
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

        // Ǯ���� ��ųʸ��� �����Ѵٸ�
        if(poolDict.ContainsKey(poolObj.ID))
        {
            poolDict[poolObj.ID].Push(poolObj);
        }
        // �������� �ʴ´ٸ� ������ ���� �����ؼ� �־���
        else
        {
            poolDict.Add(poolObj.ID, new Stack<Poolable>());
            poolDict[poolObj.ID].Push(poolObj);
        }

        if (currentActivePoolables.Contains(poolObj))
            currentActivePoolables.Remove(poolObj);
    }

    /// <summary>
    /// �Ķ���ͷ� �������� �޴� Pop
    /// </summary>
    /// <param name="poolObj"> ������ </param>
    /// <returns></returns>
    public Poolable Pop(Poolable poolObj)
    {
        // Ǯ���� ��ųʸ��� �����Ѵٸ�
        if(poolDict.ContainsKey(poolObj.ID))
        {
            // ��� �ִٸ� ����
            if(poolDict[poolObj.ID].Count > 0)
            {
                var popObj = poolDict[poolObj.ID].Pop();
                currentActivePoolables.Add(popObj);
                return popObj;
            }
            // ���ٸ� ���� �� ����
            else
            {
                var newPoolObj = Instantiate(poolObj, transform);
                newPoolObj.gameObject.SetActive(false);
                currentActivePoolables.Add(newPoolObj);
                return newPoolObj;
            }
        }
        // ��ųʸ��� ���� Ǯ�����̶��
        else
        {
            // ��ųʸ��� �ڸ��� �����
            poolDict.Add(poolObj.ID, new Stack<Poolable>());
            // ���� �� ����
            var newPoolObj = Instantiate(poolObj, transform);
            newPoolObj.gameObject.SetActive(false);
            currentActivePoolables.Add(newPoolObj);
            return newPoolObj;
        }
    }

    /// <summary>
    /// �Ķ���ͷ� ID�� �޴� Pop
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
            // ��� �����ϴٸ�
            else
            {
                // Ǯ���� ������ ����Ʈ���� ID�� ã�ƺ�
                var poolPrefab = allPoolables.Find((p) => p.ID == id);
                // �׷��� ������ ���� ��
                if (poolPrefab == null)
                    return null;

                // ���� �� ����
                var newPoolObj = Instantiate(poolPrefab, transform);
                newPoolObj.gameObject.SetActive(false);
                currentActivePoolables.Add(newPoolObj);
                return newPoolObj;
            }
        }
        // Ǯ���� ��ųʸ��� ���ٸ�
        else
        {
            // Ǯ���� ������ ����Ʈ���� ID�� ã�ƺ�
            var poolPrefab = allPoolables.Find((p) => p.ID == id);
            // �׷��� ������ ���� ��
            if (poolPrefab == null)
                return null;

            // ��ųʸ��� �ڸ��� �����
            poolDict.Add(id, new Stack<Poolable>());
            // ���� �� ����
            var newPoolObj = Instantiate(poolPrefab, transform);
            newPoolObj.gameObject.SetActive(false);
            currentActivePoolables.Add(newPoolObj);
            return newPoolObj;
        }
    }
}
