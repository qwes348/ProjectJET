using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "MissileData", menuName = "Data/MissileData")]
public class MissileData : SerializedScriptableObject
{
    [SerializeField]
    [LabelText("속도")]
    private float speed;

    [SerializeField]
    [LabelText("데미지")]
    private float damage;

    [SerializeField]
    [LabelText("최대거리")]
    private float maxRange;

    #region 프로퍼티

    public float Speed => speed;
    public float Damage => damage;
    public float MaxRange => maxRange;

    #endregion
}
