using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIIDleState : AIFSMState
{
    private float timer = 0;

    public AIIDleState(AIStateManager stateMgr)
    {
        stateManager = stateMgr;
    }

    public override void Act()
    {
        timer += Time.deltaTime;
        if (timer >= Random.Range(3, 5))
        {
            timer = 0;
            stateManager.ChangeState(new AIPatrolState(stateManager));
        }

        //切换到追击状态

        if (stateManager.Target != null &&
          Vector3.Distance(stateManager.Target.transform.position, stateManager.gameObject.transform.position) < stateManager.persueRadius
          && Vector3.Angle(stateManager.Target.transform.position - stateManager.gameObject.transform.position, stateManager.gameObject.transform.forward) < 60f
          )
        {
            stateManager.ChangeState(new AIPersueState(stateManager, stateManager.Target));
        }




    }



}
