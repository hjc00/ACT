using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle : MonoBehaviour
{

    public Steering[] steerings;

    public Vector3 velocity;  //速度
    public Vector3 steeringForce;  //操纵行为的力
    public Vector3 acceleration; //加速度

    public float mass = 1; //质量

    public float maxForce = 100;
    public float maxSpeed = 10;
    public float sqrMaxSpeed;
    public float rotateDamp = 0.8f;//转向速率

    private float computeInterval = 0.2f;
    private float timer;

    protected void Start()
    {
        steerings = GetComponents<Steering>();
        sqrMaxSpeed = maxSpeed * maxSpeed;
        timer = 0;
        steeringForce = Vector3.zero;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        steeringForce = Vector3.zero;

        if (timer > computeInterval)
        {
            foreach (Steering s in steerings)
            {
                steeringForce += s.CalculateForce() * s.weight;
            }
            //限制力的大小
            steeringForce = Vector3.ClampMagnitude(steeringForce, maxForce);

            acceleration = steeringForce / mass;
            timer = 0;
        }
    }
}
