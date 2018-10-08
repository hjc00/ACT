using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AILocomotion : Vehicle
{
    private Animator anim;
    private Rigidbody rigid;

    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
        base.Start();
    }

    private void FixedUpdate()
    {
        velocity += acceleration * Time.deltaTime;
        //cc.SimpleMove(velocity);
        rigid.velocity = velocity;

        //限制速度
        if (velocity.sqrMagnitude > sqrMaxSpeed)
        {
            velocity = velocity.normalized * maxSpeed;
        }

        //改变朝向
        if (velocity.sqrMagnitude > 0.00001f)
        {
            Vector3 newForward = Vector3.Lerp(transform.position, velocity, rotateDamp);
            transform.forward = newForward;
        }

        anim.SetFloat("forward", velocity.magnitude);
    }
}
