using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New GunData", menuName = "Data / GunData")]
public class GunData : ScriptableObject
{
    [Header("�����̸�")]
    public string gunName;
    [Header("�Ѿ� ������")]
    public Bullet bulletPrefab;
    [Header("źâ ������")]
    public int magazineSize;
    [Header("������")]
    public float damage;
    [Header("���� �ӵ�")]
    public float firerate;
    [Header("������ �ӵ�")]
    public float reloadTime;
    [Header("�ִ� ��Ÿ�")]
    public float maxRange;
    [Header("�Ѿ� �ӵ�")]
    public float bulletSpeed;
}
