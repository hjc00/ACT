using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringForEvade : Steering {

    public GameObject target;
    private float speed;
    [SerializeField]
    private Vehicle vehicle;
    private Vector3 desiredVelocity;

    private void Start()
    {
        vehicle = GetComponent<Vehicle>();
        speed = vehicle.maxSpeed;
    }

    public override Vector3 CalculateForce()
    {
      //Vector3 toTarget = target.transform.position - transform.position;
        float lookaheadTime = 0.2f;

        desiredVelocity = (transform.position -
            (target.transform.position + target.GetComponent<Rigidbody>().velocity * lookaheadTime)).normalized * speed;

        return desiredVelocity - vehicle.velocity;
    }
}
