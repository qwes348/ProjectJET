using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Poolable))]
public class ElectricLine : MonoBehaviour
{
    [SerializeField]
    private Poolable myPoolable;
    [SerializeField]
    private float damage;

    private PlatformMap parentMap;

    private bool isActivate = true;

    public void Init(Transform parentMap)
    {
        isActivate = true;

        this.parentMap = parentMap.GetComponent<PlatformMap>();
        this.parentMap.onPushed.AddListener(OnMapPushed);

        gameObject.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isActivate)
            return;
        if (!collision.transform.CompareTag("Player"))
            return;

        var damageable = collision.GetComponent<Damageable>();
        if (damageable == null)
            return;

        damageable.GetDamage(gameObject, damage);
        isActivate = false;
    }

    // �θ� ���� ȭ���� �Ѿ�� Ǯ���ƴ��� üũ
    public void OnMapPushed()
    {
        parentMap.onPushed.RemoveListener(OnMapPushed);
        PoolManager.Instance.Push(myPoolable);
    }
}
