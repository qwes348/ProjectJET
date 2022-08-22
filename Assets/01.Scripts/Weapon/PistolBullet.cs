using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolBullet : Bullet
{
    private Coroutine runningFireCor;

    public override void Init(GunWeapon gunWeapon)
    {
        myGun = gunWeapon;
        movedRange = 0f;        

        if(rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
        }

        if(myPoolable == null)
        {
            myPoolable = GetComponent<Poolable>();
        }
    }

    public override void Fire(Vector3 startPos, Vector3 forwradDir)
    {
        transform.position = startPos;
        rb.velocity = Vector3.zero;
        prevPos = startPos;

        if (!gameObject.activeSelf)
            gameObject.SetActive(true);

        if (runningFireCor != null)
            runningFireCor = null;
        runningFireCor = StartCoroutine(FireCor(forwradDir));
    }

    IEnumerator FireCor(Vector3 forwardDir)
    {
        while (true)
        {
            yield return Time.fixedDeltaTime;
            
            rb.velocity = myGun.MyGunData.bulletSpeed * Time.fixedDeltaTime * forwardDir;
            movedRange += Vector2.Distance(prevPos, transform.position);

            if (movedRange > myGun.MyGunData.maxRange)
                break;

            prevPos = transform.position;            
        }

        if(myPoolable != null)
        {
            PoolManager.Instance.Push(myPoolable);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
}
