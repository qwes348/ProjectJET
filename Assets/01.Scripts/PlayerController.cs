using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    public Transform modelTransform;
    public GunWeapon myGun;
    public float jetpackPower;
    [SerializeField]
    private float groundCheckDistance = 0.2f;
    private float currentHealth;

    [Header("µð¹ö±×")]
    [SerializeField][ReadOnly]
    private float groundDistance;
    [SerializeField][ReadOnly]
    private bool isInputJetpack;
    [SerializeField]
    private GunData testGunData;

    private Sequence damageSequence;

    public bool IsOnGround
    {
        get
        {
            return groundDistance < groundCheckDistance;
        }
    }

    private Animator anim;
    private Rigidbody2D rb;    


    private void Awake()
    {
        anim = modelTransform.GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        if(myGun != null && testGunData != null)
        {
            myGun.Init(testGunData);
        }
    }

    private void Start()
    {
        GameManager.instance.SetPlayer(this);        
    }

    private void Update()
    {
        isInputJetpack = Input.GetKey(KeyCode.W);

        if(Input.GetMouseButtonDown(0))
        {
            if (myGun != null && myGun.IsCanShoot())
                myGun.Shoot();
        }
    }

    private void FixedUpdate()
    {
        UpdateGroundDistance();

        if (isInputJetpack)
            ThrustJetpack();

        anim.SetBool("JetpackThrust", isInputJetpack);
    }

    public void Init()
    {
        currentHealth = GameManager.instance.startHealth;
        GameCanvasManager.instance.UpdatePlayerHPText(currentHealth);
    }

    private void UpdateGroundDistance()
    {
        var hit = Physics2D.Raycast(modelTransform.position, modelTransform.up * -1f, Mathf.Infinity, 1 << LayerMask.NameToLayer("Ground"));

        if (hit)
        {
            groundDistance = hit.distance;

            anim.SetBool("OnGround", IsOnGround);
        }
    }

    private void ThrustJetpack()
    {
        if (rb == null)
            return;        

        rb.AddForce(jetpackPower * Time.fixedDeltaTime * transform.up, ForceMode2D.Impulse);
    }

    public void GetDamage(GameObject attacker, float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0f)
            GameManager.instance.SetGameState(Enums.GameState.End);

        GameCanvasManager.instance.UpdatePlayerHPText(currentHealth);
        PlayDamageTween();
    }

    private void PlayDamageTween()
    {
        if (damageSequence == null)
        {
            var spr = modelTransform.GetComponent<SpriteRenderer>();
            damageSequence = DOTween.Sequence()
                .Append(spr.DOColor(Color.red, 0.2f))
                .Append(spr.DOColor(Color.white, 0.2f))
                .SetAutoKill(false);
        }
        else
            damageSequence.Restart();
    }
}
