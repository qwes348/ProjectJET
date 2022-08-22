using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPistol : GunWeapon
{
    private Coroutine runningReloadCor;

    public override void Init(GunData newGunData)
    {
        myGunData = newGunData;
        currentMagazine = newGunData.magazineSize;
    }

    public override void Reload()
    {
        if (runningReloadCor != null)
            return;

        runningReloadCor = StartCoroutine(ReloadCor());
    }

    public override bool IsCanShoot()
    {
        return currentMagazine > 0;
    }

    public override void Shoot()
    {
        if (myGunData == null)
            return;

        //Bullet bullet = Instantiate(myGunData.bulletPrefab, transform.position, Quaternion.identity);
        Bullet bullet = PoolManager.Instance.Pop(myGunData.bulletPrefab.MyPoolable).GetComponent<Bullet>();
        bullet.transform.position = transform.position;        
        bullet.Init(this);
        bullet.gameObject.SetActive(true);
        bullet.Fire(transform.position, transform.right);
    }    

    IEnumerator ReloadCor()
    {
        yield return new WaitForSeconds(MyGunData.reloadTime);

        currentMagazine = MagazineSize;

        runningReloadCor = null;
    }
}
