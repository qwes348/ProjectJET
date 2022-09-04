using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;

public class ScrollMapController : MonoBehaviour
{    
    [SerializeField] private GameObject firstMapPrefab;
    [SerializeField] private List<Poolable> mapPrefabList;

    [Space]
    [SerializeField] private List<Transform> activeMapList;

    [Space]
    public UnityEvent<Transform> onMapSpawned;

    #region ½ºÅ©·Ñ¸Ê ¼³Á¤
    [FoldoutGroup("½ºÅ©·Ñ¸Ê ¼³Á¤")] 
    [SerializeField] 
    private float minX;

    [FoldoutGroup("½ºÅ©·Ñ¸Ê ¼³Á¤")] 
    [SerializeField] 
    private float maxX;

    [FoldoutGroup("½ºÅ©·Ñ¸Ê ¼³Á¤")]
    [SerializeField]
    private float startSpeed;
    #endregion

    private void Awake()
    {
        var newMap = Instantiate(firstMapPrefab, transform);
        newMap.SetActive(true);
        activeMapList.Add(newMap.transform);
    }

    private void Update()
    {
        if (GameManager.instance.GameState != Enums.GameState.Playing)
            return;

        ScrollMap();

        if (activeMapList.Count <= 1)
        {
            SpawnRandomMap();
        }
    }

    private void ScrollMap()
    {
        List<GameObject> destroyList = new List<GameObject>();

        foreach (var map in activeMapList)
        {
            map.transform.Translate(Vector2.left * startSpeed * Time.deltaTime);

            if (map.transform.localPosition.x <= minX)
            {
                destroyList.Add(map.gameObject);
            }
        }

        if(destroyList.Count > 0)
        {
            DestroyMap(destroyList);
        }
    }

    private void DestroyMap(List<GameObject> destroyList)
    {
        foreach (var map in destroyList)
        {
            activeMapList.Remove(map.transform);
            var poolable = map.GetComponent<Poolable>();
            if(poolable == null)
                Destroy(map.gameObject);
            else
                PoolManager.Instance.Push(map.GetComponent<Poolable>());
        }
    }

    private void SpawnRandomMap()
    {
        //var newMap = Instantiate(mapPrefabList[Random.Range(0, mapPrefabList.Count)], transform);
        var newMap = PoolManager.Instance.Pop(mapPrefabList[Random.Range(0, mapPrefabList.Count)]);
        newMap.transform.SetParent(transform);
        newMap.transform.localPosition = Vector3.right * maxX;
        newMap.gameObject.SetActive(true);
        activeMapList.Add(newMap.transform);

        onMapSpawned?.Invoke(newMap.transform);
    }
}
