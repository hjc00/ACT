using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIHitState : AIFSMState
{
    private Rigidbody rigid;

    public AIHitState(AIStateManager stateMgr)
    {
        rigid = stateMgr.aiGo.GetComponent<Rigidbody>();
        stateManager = stateMgr;
    }

    public override void Act()
    {
        rigid.velocity = Vector3.zero;

        if (rigid.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime > 0.95f)
        {
            stateManager.ChangeState(stateManager.PreState);
        }
    }
}

