using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringForPursuit : Steering
{

    public Vector3 desiredVelocity;
    public GameObject target;
    public Vehicle vehicle;
    private float speed;

    private void Start()
    {
        vehicle = GetComponent<Vehicle>();
        speed = vehicle.maxSpeed;
    }

    public override Vector3 CalculateForce()
    {
        Vector3 toTarget = target.transform.position - transform.position;

        //判断追逐者是否面对target
        float faceDirection = Vector3.Dot(target.transform.forward, transform.forward);

        if (Vector3.Dot(toTarget, transform.forward) > 0 && faceDirection < -0.95f)
        {
            desiredVelocity = (target.transform.position - transform.position).normalized * speed;
            return desiredVelocity - vehicle.velocity;
        }

        //前向预测时间

        //float lookAheadTime = 0.2f;
        float lookAheaTime = toTarget.magnitude / (speed + target.GetComponent<Vehicle>().velocity.magnitude);

        desiredVelocity = ((target.transform.position + target.transform.GetComponent<Vehicle>().velocity * lookAheaTime)
            - transform.position).normalized * speed;

        return desiredVelocity - vehicle.velocity;
    }

}
