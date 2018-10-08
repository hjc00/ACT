using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AIPatrolState : AIFSMState
{
    private List<Vector3> patrolPoints = new List<Vector3>();
    private Vector3 nextPos;
    private Transform aiTrans;
    private float walkSpeed = 2f;
    private Rigidbody rigid;
    private Animator anim;

    public AIPatrolState(AIStateManager stateMgr)
    {
        stateManager = stateMgr;
        aiTrans = stateMgr.aiGo.transform;
        rigid = aiTrans.GetComponent<Rigidbody>();
        anim = aiTrans.GetComponent<Animator>();

        patrolPoints.Add(aiTrans.position + (aiTrans.forward.normalized * 10));
        patrolPoints.Add(aiTrans.position + (-aiTrans.forward.normalized * 10));
        patrolPoints.Add(aiTrans.position + (aiTrans.right.normalized * 10));
        patrolPoints.Add(aiTrans.position + (-aiTrans.right.normalized * 10));

        nextPos = patrolPoints[0];
    }

    public override void Act()
    {
        Quaternion targetRot = Quaternion.LookRotation(nextPos - aiTrans.position);
        aiTrans.rotation = Quaternion.Lerp(aiTrans.rotation, targetRot, 0.1f);

        rigid.velocity = (nextPos - aiTrans.position).normalized * walkSpeed;
        aiTrans.GetComponent<Animator>().SetBool("walk", true);
        stateManager.aiGo.GetComponent<Animator>().SetBool("run", false);

        if (Vector3.Distance(nextPos, aiTrans.position) < 2.5f)
        {
            rigid.velocity = Vector3.zero;
            nextPos = patrolPoints[Random.Range(0, patrolPoints.Count)];
        }

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("hit"))
        {
            stateManager.ChangeState(new AIHitState(stateManager));
        }

        if (stateManager.Target != null &&
            Vector3.Distance(stateManager.Target.transform.position, aiTrans.position) < stateManager.persueRadius
            && Vector3.Angle(stateManager.Target.transform.position - aiTrans.position, aiTrans.forward) < 60f
            )
        {
            stateManager.ChangeState(new AIPersueState(stateManager, stateManager.Target));
        }

    }
}

