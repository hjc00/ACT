using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringForSeek : Steering {

    public GameObject target;
    private float speed;
    private Vector3 desiredVelocity; //预期速度\
    [SerializeField]
    private Vehicle vehicle;

	void Start () {
        vehicle = GetComponent<Vehicle>();
        speed = vehicle.maxSpeed;
	}
	

    public override Vector3 CalculateForce()
    {
        desiredVelocity = (target.transform.position - transform.position).normalized * speed;
        return desiredVelocity - vehicle.velocity;
    }
}
