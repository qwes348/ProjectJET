using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollMapController : MonoBehaviour
{
    [SerializeField] private float outScreenX;
    [SerializeField] private List<GameObject> mapPrefabList;

    [Space]
    [SerializeField] private List<Transform> activeMapList;
}
