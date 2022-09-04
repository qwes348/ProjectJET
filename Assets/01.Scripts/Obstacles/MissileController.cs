using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class MissileController : MonoBehaviour
{
    [SerializeField]
    [InlineEditor]
    private MissileData myData;

    private Damageable detectedTarget;
    private Rigidbody2D rb;
    private Coroutine runningFireCor;
    private Poolable myPoolable;

    public void Init(MissileData newData = null)
    {
        if (rb == null)
            rb = GetComponent<Rigidbody2D>();

        if (myPoolable == null)
            myPoolable = GetComponent<Poolable>();

        detectedTarget = null;
        runningFireCor = null;

        if (newData != null)
            myData = newData;
    }

    public void Fire(Vector3 dir)
    {
        if (runningFireCor != null)
            StopCoroutine(runningFireCor);

        runningFireCor = StartCoroutine(FireCor(dir));
    }

    IEnumerator FireCor(Vector3 dir)
    {
        float range = 0f;
        Vector2 prevPos = transform.position;
        while (true)
        {            
            if(range >= myData.MaxRange)
            {
                StopCoroutine(runningFireCor);
                PoolManager.Instance.Push(myPoolable);
            }

            //rb.MovePosition(myData.Speed * Time.fixedDeltaTime * dir);
            rb.velocity = myData.Speed * Time.fixedDeltaTime * dir;
            range += Vector2.Distance(prevPos, transform.position);
            prevPos = transform.position;
            yield return Time.fixedDeltaTime;
        }
    }

    private void DamageToTarget()
    {
        if (detectedTarget == null)
            return;

        detectedTarget.GetDamage(gameObject, myData.Damage);
        PoolManager.Instance.Push(myPoolable);
    }

    public void GetDamage(GameObject attacker, float damage)
    {
        if (runningFireCor != null)
            StopCoroutine(runningFireCor);

        PoolManager.Instance.Push(myPoolable);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (runningFireCor == null)
            return;
        if (!collision.gameObject.CompareTag("Player"))
            return;
        var damageable = collision.GetComponent<Damageable>();
        if (damageable == null)
            return;

        detectedTarget = damageable;

        StopCoroutine(runningFireCor);
        runningFireCor = null;

        DamageToTarget();
    }
}
