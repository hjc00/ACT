using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : EntityController
{
    [SerializeField]
    private GameObject playerGo;
    private AIStateManager stateManager;
    private AIBattleManager AIbattleManager;

    public float health = 100;
    public bool dead = false;

    public GameObject PlayerGo { get { return playerGo; } }
    public AIStateManager StateManager { get { return stateManager; } }
    public float Health { get { return health; } }

    private void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();
        stateManager = GetComponent<AIStateManager>();
        AIbattleManager = GetComponentInChildren<AIBattleManager>();
        fxManager = GetComponent<FxManager>();
        audioManager = GetComponent<AudioManager>();

        stateManager.SetController(this);
        AIbattleManager.SetAIController(this);
        fxManager.SetEntityController(this);
        audioManager.SetEntityController(this);
    }

    private void Update()
    {
        if (!dead)
            stateManager.Act();
    }

    public override void SetTrigger(string name)
    {
        anim.SetTrigger(name);
    }

    public override void PlayBloodFx(Vector3 pos)
    {
        fxManager.PlayBloodFx(pos);
    }

    public override void PlayHitSound()
    {
        audioManager.PlayHitSound();
    }

    public override void PlayAtkSound()
    {
        base.PlayAtkSound();
    }

    public void ChangeState(AIFSMState state)
    {
        stateManager.ChangeState(state);
    }

    public void SetPlayerGo(GameObject go)  //设置AI的目标
    {
        if (playerGo == null)
            playerGo = go;
        stateManager.SetTarget(go);
    }

    public override void DoDamage(float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            dead = true;
            stateManager.enabled = false;  //取消AI的移动
            AIbattleManager.DefenseCol.enabled = false;
            AIbattleManager.enabled = false;
            anim.SetTrigger("die");
            this.enabled = false;
        }
    }
    //private void TurnAround(vec)
    //{

    //}


    ///动画事件
    ///
    //public void SetVelocityZero()
    //{
    //    rigid.velocity = Vector3.zero;
    //}

    public void EnableAtkSphere()
    {
        AIbattleManager.EnableAtkSphere();
    }

    public void DisableAtkSphere()
    {
        AIbattleManager.DisableAtkSphere();
    }

    public IEnumerator SinkAfterDead()
    {
        float amount = 0;
        while (this.transform.position.y >= -2)
        {
            amount += 0.01f;
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - amount, this.transform.position.z);
            yield return new WaitForFixedUpdate();

        }
        Destroy(this.gameObject, 2f);
    }


    ////FSM call back
    ///
    public void SetVelocityZero()
    {
        rigid.velocity = Vector3.zero;
    }

    public void ResetTrigger()
    {
        anim.ResetTrigger("attack");
    }

    public void ResetHitTrigger()
    {
        anim.ResetTrigger("hit");
    }
}
