using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
    [SerializeField]
    private bool isDead;
    public UnityEvent<GameObject, float> onGetDamaged;

    public bool IsDead
    {
        get => isDead;
        set
        {
            isDead = value;
        }
    }

    public void GetDamage(GameObject attacker, float damage)
    {
        if(!isDead)
            onGetDamaged?.Invoke(attacker, damage);
    }
}
