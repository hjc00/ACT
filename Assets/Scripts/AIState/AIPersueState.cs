using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPersueState : AIFSMState
{
    public GameObject target;
    private float attackRadius = 1f; //攻击范围
    private float maxPersueDist = 20f; //最大追逐距离

    public AIPersueState(AIStateManager stateMgr, GameObject gameObject)
    {
        stateManager = stateMgr;
        target = gameObject;
    }

    public override void Act()
    {
        //if (stateManager.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("run"))
       // {
            Quaternion targetRot = Quaternion.LookRotation(target.transform.position - stateManager.gameObject.transform.position);
            stateManager.transform.rotation = Quaternion.Slerp(stateManager.transform.rotation, targetRot, 0.35f);
       // }

        //求出速度
        stateManager.GetComponent<Rigidbody>().velocity = (target.transform.position -
            stateManager.aiGo.transform.position).normalized * stateManager.runSpeed;

        stateManager.GetComponent<Animator>().SetBool("run", true);
        stateManager.GetComponent<Animator>().SetBool("walk", false);

        //切换追击状态
        if (Vector3.Distance(stateManager.transform.position, target.transform.position) > maxPersueDist)
        {
            //stateManager.ChangeState(stateManager.PreState);
            stateManager.ChangeState(new AIPatrolState(stateManager));
        }

        //切换到攻击状态
        if (Vector3.Distance(stateManager.transform.position, target.transform.position) < stateManager.attackRadius)
        {
            stateManager.ChangeState(new AIAttackState(stateManager));
        }

    }
}
