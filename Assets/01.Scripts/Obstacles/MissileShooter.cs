using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class MissileShooter : MonoBehaviour
{
    public List<Transform> missileFirePosList;

    [Title("ÇÁ¸®ÆÕ")]
    public Poolable missilePrefab;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            FireMissile();
    }

    public void FireMissile()
    {
        MissileController missile = PoolManager.Instance.Pop(missilePrefab).GetComponent<MissileController>();

        var firePos = missileFirePosList[Random.Range(0, missileFirePosList.Count)];
        missile.transform.position = firePos.transform.position;
        missile.transform.rotation = firePos.transform.rotation;
        missile.gameObject.SetActive(true);
        missile.Init();
        missile.Fire(firePos.transform.right);
    }
}
