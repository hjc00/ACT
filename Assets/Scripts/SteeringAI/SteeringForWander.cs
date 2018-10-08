using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringForWander : Steering
{

    public float wanderDistance;
    public float wanderRadius;
    public float wanderJitter;
    private Vector3 wanderTarget; //漫游位置
    private Vector3 circleTarget;  //圆周位置

    private Vector3 desiredVelocity;
    private Vehicle vehicle;
    private float speed;

    private void Start()
    {
        vehicle = GetComponent<Vehicle>();
        speed = vehicle.maxSpeed;

        //随机初始化起始位置
        circleTarget = new Vector3(wanderRadius * Random.value,
            0, wanderRadius * Random.value);
    }

    public override Vector3 CalculateForce()
    {
        Vector3 randomPlacement = new Vector3((Random.value-0.5f) * 2 * wanderJitter,
           (Random.value - 0.5f) * 2 * wanderJitter,
           (Random.value - 0.5f) * 2 * wanderJitter);

        circleTarget += randomPlacement;

        //投射到圆上
        circleTarget.y = 0;
        circleTarget = wanderRadius * circleTarget.normalized;
        wanderTarget = circleTarget + vehicle.velocity.normalized * wanderDistance + transform.position;

        desiredVelocity = (wanderTarget - transform.position).normalized * speed;
        return desiredVelocity - vehicle.velocity;
    }


}
