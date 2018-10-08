using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringForCohension : Steering
{

    private Radar radar;
    void Start()
    {
        radar = GetComponent<Radar>();
    }

    public override Vector3 CalculateForce()
    {
        Vector3 steeringForce = Vector3.zero;
        Vector3 center = Vector3.zero;
        int count = 0;
        foreach (GameObject g in radar.GetNeighbors())
        {
            if (g != null && g != this.gameObject)
            {
                center += g.transform.position;
                count++;
            }
        }
        if (count > 0)
        {
            center /= count;
             steeringForce = (center - transform.position).normalized * 5 - GetComponent<Vehicle>().velocity;
           // steeringForce = center - transform.position;
        }
        return steeringForce;
    }
}
