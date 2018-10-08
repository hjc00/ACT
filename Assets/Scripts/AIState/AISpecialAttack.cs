using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISpecialAttack : AIFSMState
{
    public AISpecialAttack(AIStateManager s)
    {
        stateManager = s;
    }

    public override void Act()
    {
        stateManager.GetComponent<Rigidbody>().velocity = Vector3.zero;
        stateManager.GetComponent<Animator>().SetTrigger("sattack");

        if ((stateManager.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsTag("sattack") &&
            stateManager.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime > 0.8f))
        {
            stateManager.ChangeState(new AIPersueState(stateManager, stateManager.Target));
            stateManager.ResetAngryAmount();
            Debug.Log("reset1");
        }

        //超出攻击范围
        if (Vector3.Distance(stateManager.transform.position, stateManager.Target.transform.position) > stateManager.attackRadius
            && stateManager.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime > 0.8f)
        {
            stateManager.ChangeState(new AIPersueState(stateManager, stateManager.Target));
            stateManager.ResetAngryAmount();
            Debug.Log("reset2");
        }
    }
}
