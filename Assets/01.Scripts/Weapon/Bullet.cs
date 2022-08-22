using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public abstract class Bullet : MonoBehaviour
{
    [LabelText("RigidBody")]
    public Rigidbody2D rb;
    public GunWeapon MyGun
    {
        get => myGun == null ? null : myGun;
    }

    public Poolable MyPoolable => myPoolable;

    protected GunWeapon myGun;
    protected float damage;
    protected float maxRange;
    protected float movedRange;
    protected Vector2 prevPos;
    [SerializeField]
    protected Poolable myPoolable;

    public abstract void Init(GunWeapon myGun);

    public abstract void Fire(Vector3 startPos, Vector3 forwradDir);
}
