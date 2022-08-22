using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public abstract class GunWeapon : MonoBehaviour
{
    public GunData MyGunData
    {
        get => myGunData;
    }

    public int MagazineSize
    {
        get => myGunData == null ? -1 : myGunData.magazineSize;
    }

    protected GunData myGunData;
    [SerializeField][ReadOnly]
    protected int currentMagazine;

    public abstract bool IsCanShoot();

    public abstract void Shoot();

    public abstract void Reload();

    public abstract void Init(GunData newGunData);
}
