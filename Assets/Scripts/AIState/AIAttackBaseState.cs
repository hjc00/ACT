using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAttackBaseState : AIFSMState
{
    public AIAttackBaseState(AIStateManager s)
    {
        stateManager = s;
        stateManager.GetComponent<Animator>().SetBool("run", true);
    }

    public override void Act()
    {
        stateManager.GetComponent<Rigidbody>().velocity = (BasementManager.Instance.transform.position -
            stateManager.transform.position).normalized * stateManager.runSpeed;

        if (Vector3.Distance(stateManager.transform.position, BasementManager.Instance.transform.position) < stateManager.attackRadius)
        {
            stateManager.GetComponent<Rigidbody>().velocity = Vector3.zero;
            stateManager.GetComponent<Animator>().SetTrigger("attack");
        }

        if (stateManager.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("hit"))
        {
            stateManager.ChangeState(new AIHitState(stateManager));
        }


        if (stateManager.Target != null &&
        Vector3.Distance(stateManager.Target.transform.position, stateManager.transform.position) < stateManager.persueRadius
        && Vector3.Angle(stateManager.Target.transform.position - stateManager.transform.position, stateManager.transform.forward) < 60f
        )
        {
            stateManager.ChangeState(new AIPersueState(stateManager, stateManager.Target));
        }
    }
}
