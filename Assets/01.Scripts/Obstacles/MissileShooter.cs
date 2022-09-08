using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using DG.Tweening;

public class MissileShooter : MonoBehaviour
{
    public List<Transform> missileAlertList;

    [Title("ÇÁ¸®ÆÕ")]
    public Poolable missilePrefab;

    private void Start()
    {
        Init();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            FireMissile();
    }

    public void Init()
    {
        Vector3 alertPos = Camera.main.ViewportToWorldPoint(new Vector3(0.96f, 0.5f));
        alertPos.z = 0f;

        foreach(var alert in missileAlertList)
        {
            alert.position = alertPos;
            //alert.GetComponent<SpriteRenderer>().enabled = false;
            alert.gameObject.SetActive(false);
        }
    }

    public void FireMissile(int count = 1)
    {
        StartCoroutine(ReadyFireMissileCor(count));
    }

    IEnumerator ReadyFireMissileCor(int fireCount)
    {
        for (int i = 0; i < fireCount; i++)
        {
            StartCoroutine(FireMissileCor());
            yield return new WaitForSeconds(1f);
        }
    }

    IEnumerator FireMissileCor()
    {
        var alert = missileAlertList.Find(a => !a.gameObject.activeSelf);

        if(alert == null)
        {
            alert = Instantiate(missileAlertList[0], missileAlertList[0].transform.parent);
            missileAlertList.Add(alert);
        }

        alert.gameObject.SetActive(true);

        float delayTimer = 0f;

        while(delayTimer <= 1.5f)
        {
            //missileAlertTr.transform.DOMoveY(GameManager.instance.Player.transform.position.y, Time.deltaTime);
            Vector3 movePos = new Vector3(alert.transform.position.x, GameManager.instance.Player.transform.position.y, 0f);
            alert.transform.position = Vector3.Lerp(alert.transform.position, movePos, Time.deltaTime * 2f);
            delayTimer += Time.deltaTime;

            yield return null;
        }

        alert.gameObject.SetActive(false);
        MissileController missile = PoolManager.Instance.Pop(missilePrefab).GetComponent<MissileController>();

        missile.transform.position = alert.transform.position - alert.transform.right;
        missile.transform.rotation = alert.transform.rotation;
        missile.gameObject.SetActive(true);
        missile.Init();
        missile.Fire(alert.transform.right);
    }
}
