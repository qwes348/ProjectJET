using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "MissileData", menuName = "Data/MissileData")]
public class MissileData : SerializedScriptableObject
{
    [SerializeField]
    [LabelText("�ӵ�")]
    private float speed;

    [SerializeField]
    [LabelText("������")]
    private float damage;

    [SerializeField]
    [LabelText("�ִ�Ÿ�")]
    private float maxRange;

    #region ������Ƽ

    public float Speed => speed;
    public float Damage => damage;
    public float MaxRange => maxRange;

    #endregion
}
