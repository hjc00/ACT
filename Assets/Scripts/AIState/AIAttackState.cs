using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAttackState : AIFSMState
{
    public AIAttackState(AIStateManager s)
    {
        stateManager = s;
    }

    public override void Act()
    {
        stateManager.gameObject.transform.LookAt(stateManager.Target.transform.position);

        stateManager.GetComponent<Rigidbody>().velocity = Vector3.zero;
        stateManager.GetComponent<Animator>().SetTrigger("attack");

        if (stateManager.AngryAmount >= 5)
        {
            stateManager.ChangeState(new AISpecialAttack(stateManager));
        }

        if (Vector3.Distance(stateManager.transform.position, stateManager.Target.transform.position) > stateManager.attackRadius
            && stateManager.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime > 0.8f)
        {
            stateManager.ChangeState(new AIPersueState(stateManager, stateManager.Target));
        }

        if (stateManager.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime > 0.8f)
        {
            stateManager.ChangeState(new AIPersueState(stateManager, stateManager.Target));
            stateManager.GetComponent<Animator>().ResetTrigger("hit");
        }
    }
}
