using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(Poolable))]
public class LazerObstacle : MonoBehaviour
{
    public SpriteRenderer leftShooter;
    public SpriteRenderer rightShooter;
    public ParticleSystem lazerVFX;

    private Coroutine runningFireCor;
    private Poolable myPoolable;
    private float leftShooterOriginPosX = 0f;
    private float rightShooterOriginPosX = 0f;

    public void Init()
    {
        leftShooter.color = Color.white;
        rightShooter.color = Color.white;

        if(leftShooterOriginPosX == 0f)
            leftShooterOriginPosX = leftShooter.transform.localPosition.x;
        if(rightShooterOriginPosX == 0f)
            rightShooterOriginPosX = rightShooter.transform.localPosition.x;

        leftShooter.transform.DOLocalMoveX(leftShooterOriginPosX - 10f, 0f);
        rightShooter.transform.DOLocalMoveX(rightShooterOriginPosX + 10f, 0f);

        if (myPoolable == null)
            myPoolable = GetComponent<Poolable>();

        if (runningFireCor != null)
            StopCoroutine(runningFireCor);

        gameObject.SetActive(true);
    }

    public void Fire(float delay = 1f)
    {
        if (runningFireCor != null)
            StopCoroutine(runningFireCor);

        runningFireCor = StartCoroutine(FireCor(delay));
    }

    IEnumerator FireCor(float delay)
    {
        leftShooter.transform.DOLocalMoveX(leftShooterOriginPosX, 1f);
        rightShooter.transform.DOLocalMoveX(rightShooterOriginPosX, 1f);

        yield return new WaitForSeconds(1f);

        leftShooter.DOColor(Color.red, delay);
        rightShooter.DOColor(Color.red, delay);

        yield return new WaitForSeconds(delay);

        lazerVFX.Play();

        yield return new WaitForSeconds(0.5f);

        // TODO: 데미지 루틴
        var result = Physics2D.OverlapBox(transform.position, new Vector2(18f, 1f), 0f, LayerMask.GetMask("Player"));
        if(result != null)
        {
            //print(result);  // model
            var dam = result.GetComponent<Damageable>();
            if (dam != null)
            {
                dam.GetDamage(gameObject, 1f);
            }                
        }

        leftShooter.transform.DOLocalMoveX(leftShooterOriginPosX - 10f, 1f);
        rightShooter.transform.DOLocalMoveX(rightShooterOriginPosX + 10f, 1f);

        yield return new WaitForSeconds(1f);

        runningFireCor = null;

        PoolManager.Instance.Push(myPoolable);
    }
}
