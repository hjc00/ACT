using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIStateManager : MonoBehaviour
{
    public float attackRadius = 2f; //攻击范围
    public float persueRadius = 8f;//追击范围
    public float runSpeed = 3.5f;
    [SerializeField]
    private float angryAmount = 0f; //愤怒值

    private AIFSMState state;
    private AIController aIController;
    public GameObject aiGo;  //AI物体
    [SerializeField]
    private GameObject target;

    private AIFSMState preState; //用于保存之前的状态

    public AIFSMState PreState { get { return preState; } }
    public GameObject Target { get { return target; } }
    public AIFSMState State { get { return state; } }
    public float AngryAmount { get { return angryAmount; } }

    public void Start()
    {
        aiGo = this.gameObject;
        state = new AIAttackBaseState(this);
        preState = state;
    }

    public void ChangeState(AIFSMState newState)
    {
        preState = state;
        state = newState;
    }

    public void Act()
    {
        state.Act();
    }

    public void SetTarget(GameObject g)
    {
        target = g;
    }

    public void ClearTarget()
    {
        target = null;
    }

    public void SetController(AIController c)
    {
        aIController = c;
    }

    public void UpdateAngryAmount(float amount)
    {
        angryAmount += amount;
    }

    public void ResetAngryAmount()
    {
        angryAmount = 0;
    }
}
