using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New GunData", menuName = "Data / GunData")]
public class GunData : ScriptableObject
{
    [Header("무기이름")]
    public string gunName;
    [Header("총알 프리팹")]
    public Bullet bulletPrefab;
    [Header("탄창 사이즈")]
    public int magazineSize;
    [Header("데미지")]
    public float damage;
    [Header("연사 속도")]
    public float firerate;
    [Header("재장전 속도")]
    public float reloadTime;
    [Header("최대 사거리")]
    public float maxRange;
    [Header("총알 속도")]
    public float bulletSpeed;

    [Header("한번에 나올 수 있는 최대총알 수")]
    public int maxBulletCount;
}
