using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : EntityController
{
    public float health = 100;
    public float attackPower = 20;
    public float heavyAttackPower = 35;


    private Vector3 movement;
    // private float movementMagnitude;

    public Transform camTrans;

    public float moveSpeed = 4f;
    public bool inputEnable = true;
    private PlayerBattleManager battleManager;

    void Awake()
    {
        Init();
    }

    public override void Init()
    {
        rigid = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();

        battleManager = GetComponentInChildren<PlayerBattleManager>();
        audioManager = GetComponent<AudioManager>();
        animationsEventManager = GetComponentInChildren<AnimationsEventManager>();
        fxManager = GetComponent<FxManager>();


        battleManager.SetPlayerController(this);
        audioManager.SetEntityController(this);
        animationsEventManager.SetPlayerController(this);
        fxManager.SetEntityController(this);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            anim.SetTrigger("attack");
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            anim.SetTrigger("sattack");
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetTrigger("roll");
        }

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        if (inputEnable)
        {
            movement = h * camTrans.right + v * camTrans.forward;
            rigid.velocity = movement * moveSpeed;
            rigid.isKinematic = false;
            anim.SetFloat("forward", Mathf.Lerp(anim.GetFloat("forward"), movement.magnitude, 0.4f));
            transform.forward = Vector3.Slerp(transform.forward, movement, 0.2f);
        }
        if (movement == Vector3.zero)
        {
            rigid.isKinematic = true;
        }
    }


    public override void SetTrigger(string name)
    {
        anim.SetTrigger(name);
        if (name == "hit" && anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.4f)
        {
            anim.speed = 0.5f;
            StartCoroutine(ResetAnimSpeed());
        }
    }

    public override void DoDamage(float amount)
    {
        health -= amount;
        UIManager.Instance.UpdateHealthBar(health);
        if (health <= 0)
        {
            anim.SetTrigger("die");
            battleManager.DefenseCol.enabled = false;
            this.enabled = false;
        }
    }

    IEnumerator ResetAnimSpeed()
    {
        yield return new WaitForSeconds(0.2f);
        anim.speed = 1;
    }

    public override void PlayAtkSound()
    {
        audioManager.PlayAtkSound();
    }

    public override void PlayHitSound()
    {
        audioManager.PlayHitSound();
    }

    public override void PlayBloodFx(Vector3 pos)
    {
        fxManager.PlayBloodFx(pos);
    }

    //FSM callback
    private void OnAttackEnter()
    {
        inputEnable = false;
        rigid.isKinematic = true;
        rigid.velocity = Vector3.zero;
    }

    private void OnAttackExit()
    {
        inputEnable = true;
    }

    private void OnGroundEnter()
    {
        inputEnable = true;
        anim.speed = 1;
        battleManager.EnableCollider();
        rigid.drag = 2;
    }

    private void OnRollEnter()
    {
        rigid.velocity = Vector3.zero;
        inputEnable = false;
    }

    private void OnHitEnter()
    {
        rigid.velocity = Vector3.zero;
        inputEnable = false;
    }

    private void OnFallDownEnter()
    {
        battleManager.DisableCollider();
        rigid.velocity = Vector3.zero;
        rigid.drag = 1000;
    }
}